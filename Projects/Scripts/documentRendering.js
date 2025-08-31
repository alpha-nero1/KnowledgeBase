/**
 * Advanced file system operations and automation
 * Useful for: Code generation, file processing, backup systems
 */

/**
 * Factory function that creates a file system automation object
 */
const createFileSystemAutomation = (rootPath = process.cwd()) => {
    // Private state captured in closure
    const state = {
        rootPath: rootPath
    };

    /**
     * Private helper function to walk directories (closure over state)
     */
    const createFileWalker = () => {
        return async function walk(dir, pattern, files = []) {
            const entries = await fs.readdir(dir, { withFileTypes: true });
            
            for (const entry of entries) {
                const fullPath = path.join(dir, entry.name);
                
                if (entry.isDirectory() && !entry.name.startsWith('.')) {
                    await walk(fullPath, pattern, files);
                } else if (entry.isFile() && new RegExp(pattern).test(entry.name)) {
                    files.push(fullPath);
                }
            }
            
            return files;
        };
    };

    /**
     * Create a file processor function with error handling
     */
    const createFileProcessor = () => {
        const walker = createFileWalker();
        
        return async (pattern, operation) => {
            console.log(`🔄 Processing files matching: ${pattern}`);
            
            const files = await walker(state.rootPath, pattern);
            const results = [];
            
            for (const file of files) {
                try {
                    const result = await operation(file);
                    results.push({ file, result, status: 'success' });
                    console.log(`✅ Processed: ${file}`);
                } catch (error) {
                    results.push({ file, error: error.message, status: 'error' });
                    console.error(`❌ Failed to process: ${file}`, error.message);
                }
            }
            
            return results;
        };
    };

    /**
     * Create a file finder function
     */
    const createFileFinder = () => {
        const walker = createFileWalker();
        
        return async (pattern) => {
            return walker(state.rootPath, pattern);
        };
    };

    /**
     * Create a template engine function
     */
    const createTemplateEngine = () => {
        // Private template processing function
        const processTemplate = (template, variables) => {
            let content = template;
            for (const [key, value] of Object.entries(variables)) {
                const regex = new RegExp(`{{\\s*${key}\\s*}}`, 'g');
                content = content.replace(regex, value);
            }
            return content;
        };

        return async (templatePath, outputPath, variables) => {
            console.log(`🔧 Generating ${outputPath} from template...`);
            
            const template = await fs.readFile(templatePath, 'utf8');
            const content = processTemplate(template, variables);
            
            // Ensure directory exists
            await fs.mkdir(path.dirname(outputPath), { recursive: true });
            await fs.writeFile(outputPath, content, 'utf8');
            
            console.log(`✅ Generated: ${outputPath}`);
        };
    };

    /**
     * Create a backup function with timestamp generation
     */
    const createBackupManager = () => {
        // Private timestamp generator
        const generateTimestamp = () => {
            return new Date().toISOString().replace(/[:.]/g, '-');
        };

        return async (sourcePath, backupDir) => {
            const timestamp = generateTimestamp();
            const backupName = `backup-${timestamp}.tar.gz`;
            const backupPath = path.join(backupDir, backupName);
            
            console.log(`💾 Creating backup: ${backupPath}`);
            
            await fs.mkdir(backupDir, { recursive: true });
            
            await execPromise(
                `tar -czf "${backupPath}" -C "${path.dirname(sourcePath)}" "${path.basename(sourcePath)}"`
            );
            
            console.log(`✅ Backup created: ${backupPath}`);
            return backupPath;
        };
    };

    // Return public API
    return {
        // Expose the state getter for debugging
        getRootPath: () => state.rootPath,
        setRootPath: (newPath) => { state.rootPath = newPath; },
        
        // Main API methods created
        processFiles: createFileProcessor(),
        findFiles: createFileFinder(),
        generateFromTemplate: createTemplateEngine(),
        createBackup: createBackupManager()
    };
};