// =====================================
// 4. 🔄 FILE SYSTEM AUTOMATION
// =====================================

/**
 * Advanced file system operations and automation
 * Useful for: Code generation, file processing, backup systems
 */
class FileSystemAutomation {
    constructor(rootPath = process.cwd()) {
        this.rootPath = rootPath;
    }
    
    /**
     * Mass file operations with pattern matching
     */
    async processFiles(pattern, operation) {
        console.log(`🔄 Processing files matching: ${pattern}`);
        
        const files = await this.findFiles(pattern);
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
    }
    
    async findFiles(pattern) {
        const files = [];
        
        async function walk(dir) {
            const entries = await fs.readdir(dir, { withFileTypes: true });
            
            for (const entry of entries) {
                const fullPath = path.join(dir, entry.name);
                
                if (entry.isDirectory() && !entry.name.startsWith('.')) {
                    await walk(fullPath);
                } else if (entry.isFile() && new RegExp(pattern).test(entry.name)) {
                    files.push(fullPath);
                }
            }
        }
        
        await walk(this.rootPath);
        return files;
    }
    
    /**
     * Generate code files from templates
     */
    async generateFromTemplate(templatePath, outputPath, variables) {
        console.log(`🔧 Generating ${outputPath} from template...`);
        
        const template = await fs.readFile(templatePath, 'utf8');
        
        // Simple template engine
        let content = template;
        for (const [key, value] of Object.entries(variables)) {
            const regex = new RegExp(`{{\\s*${key}\\s*}}`, 'g');
            content = content.replace(regex, value);
        }
        
        // Ensure directory exists
        await fs.mkdir(path.dirname(outputPath), { recursive: true });
        await fs.writeFile(outputPath, content, 'utf8');
        
        console.log(`✅ Generated: ${outputPath}`);
    }
    
    /**
     * Create backup with compression
     */
    async createBackup(sourcePath, backupDir) {
        const timestamp = new Date().toISOString().replace(/[:.]/g, '-');
        const backupName = `backup-${timestamp}.tar.gz`;
        const backupPath = path.join(backupDir, backupName);
        
        console.log(`💾 Creating backup: ${backupPath}`);
        
        await fs.mkdir(backupDir, { recursive: true });
        
        const { stdout } = await execPromise(
            `tar -czf "${backupPath}" -C "${path.dirname(sourcePath)}" "${path.basename(sourcePath)}"`
        );
        
        console.log(`✅ Backup created: ${backupPath}`);
        return backupPath;
    }
}