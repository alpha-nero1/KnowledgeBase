/**
 * CodeBreakdown (codeBreakdown.js) - Analyze code statistics using cloc
 * Uses the cloc (Count Lines of Code) tool to provide detailed breakdowns
 * of code metrics in project directories
 */

const { exec } = require('child_process');
const { promisify } = require('util');
const path = require('path');
const fs = require('fs').promises;

const execAsync = promisify(exec);

/**
 * Uses cloc to analyze code statistics in a directory
 * @param {string} srcPath - Path to the source directory to analyze (defaults to './src')
 * @param {object} options - Configuration options
 * @returns {Promise<object>} - Parsed code breakdown statistics
 */
const codeBreakdown = async (srcPath = './src', options = {}) => {
    const {
        format = 'json',           // Output format: json, csv, xml, yaml
        excludeDirs = [            // Directories to exclude
            'node_modules',
            '.git',
            'dist',
            'build',
            'coverage',
            '.next',
            'vendor'
        ],
        excludeExtensions = [],    // File extensions to exclude (e.g., ['.min.js', '.bundle.js'])
        includeExtensions = [],    // Only include these extensions (if specified)
        verbose = false,           // Enable verbose output
        showProgress = true        // Show progress during analysis
    } = options;

    try {
        // Check if source directory exists
        const resolvedPath = path.resolve(srcPath);
        await fs.access(resolvedPath);
        
        console.log(`📊 Analyzing code in: ${resolvedPath}`);
        
        // Build cloc command
        let clocCommand = `cloc "${resolvedPath}"`;
        
        // Add format option
        if (format === 'json') {
            clocCommand += ' --json';
        } else if (format === 'csv') {
            clocCommand += ' --csv';
        } else if (format === 'xml') {
            clocCommand += ' --xml';
        } else if (format === 'yaml') {
            clocCommand += ' --yaml';
        }
        
        // Add exclude directories
        if (excludeDirs.length > 0) {
            const excludeList = excludeDirs.join(',');
            clocCommand += ` --exclude-dir="${excludeList}"`;
        }
        
        // Add exclude extensions
        if (excludeExtensions.length > 0) {
            const excludeExt = excludeExtensions.map(ext => ext.replace('.', '')).join(',');
            clocCommand += ` --exclude-ext="${excludeExt}"`;
        }
        
        // Add include extensions (only these will be counted)
        if (includeExtensions.length > 0) {
            const includeExt = includeExtensions.map(ext => ext.replace('.', '')).join(',');
            clocCommand += ` --include-ext="${includeExt}"`;
        }
        
        // Add verbose flag
        if (verbose) {
            clocCommand += ' --v';
        }
        
        // Add progress indicator
        if (showProgress) {
            clocCommand += ' --progress-rate=10';
        }
        
        console.log(`🔍 Running: ${clocCommand}`);
        
        // Execute cloc command
        const { stdout, stderr } = await execAsync(clocCommand, {
            timeout: 60000 // 60 second timeout
        });
        
        if (stderr && verbose) {
            console.warn('⚠️ cloc warnings:', stderr);
        }
        
        // Parse output based on format
        let result;
        if (format === 'json') {
            result = JSON.parse(stdout);
            result = enhanceJsonOutput(result, resolvedPath);
        } else {
            result = {
                rawOutput: stdout,
                path: resolvedPath,
                format: format
            };
        }
        
        console.log('✅ Code analysis complete!');
        return result;
        
    } catch (error) {
        if (error.code === 'ENOENT') {
            throw new Error(`Directory not found: ${srcPath}. Please ensure the path exists.`);
        } else if (error.message.includes('cloc: command not found')) {
            throw new Error('cloc is not installed. Install it with: npm install -g cloc or brew install cloc');
        } else if (error.signal === 'SIGTERM') {
            throw new Error('cloc analysis timed out. Try analyzing a smaller directory or increase timeout.');
        } else {
            throw new Error(`Code analysis failed: ${error.message}`);
        }
    }
};

/**
 * Enhances JSON output with additional statistics and formatting
 */
const enhanceJsonOutput = (clocResult, analyzedPath) => {
    const enhanced = {
        summary: {
            analyzedPath,
            timestamp: new Date().toISOString(),
            totalFiles: clocResult.header?.n_files || 0,
            totalLines: clocResult.header?.n_lines || 0,
            linesOfCode: clocResult.SUM?.code || 0,
            commentLines: clocResult.SUM?.comment || 0,
            blankLines: clocResult.SUM?.blank || 0
        },
        languages: {},
        raw: clocResult
    };
    
    // Process language breakdown
    Object.keys(clocResult).forEach(key => {
        if (key !== 'header' && key !== 'SUM') {
            const lang = clocResult[key];
            enhanced.languages[key] = {
                files: lang.nFiles || 0,
                lines: lang.code || 0,
                comments: lang.comment || 0,
                blanks: lang.blank || 0,
                percentage: enhanced.summary.linesOfCode > 0 
                    ? ((lang.code || 0) / enhanced.summary.linesOfCode * 100).toFixed(1)
                    : '0.0'
            };
        }
    });
    
    return enhanced;
};

/**
 * Pretty print code breakdown results
 */
const printCodeBreakdown = (breakdown) => {
    if (!breakdown.summary) {
        console.log(breakdown.rawOutput || breakdown);
        return;
    }
    
    const { summary, languages } = breakdown;
    
    console.log('\n📊 CODE BREAKDOWN SUMMARY');
    console.log('='.repeat(50));
    console.log(`📁 Path: ${summary.analyzedPath}`);
    console.log(`📅 Analyzed: ${new Date(summary.timestamp).toLocaleString()}`);
    console.log(`📄 Total Files: ${summary.totalFiles.toLocaleString()}`);
    console.log(`📏 Total Lines: ${summary.totalLines.toLocaleString()}`);
    console.log(`💻 Lines of Code: ${summary.linesOfCode.toLocaleString()}`);
    console.log(`💬 Comment Lines: ${summary.commentLines.toLocaleString()}`);
    console.log(`⬜ Blank Lines: ${summary.blankLines.toLocaleString()}`);
    
    console.log('\n🌐 LANGUAGES BREAKDOWN');
    console.log('='.repeat(50));
    
    // Sort languages by lines of code
    const sortedLanguages = Object.entries(languages)
        .sort(([,a], [,b]) => b.lines - a.lines);
    
    sortedLanguages.forEach(([lang, stats]) => {
        console.log(`${lang.padEnd(15)} | ${stats.files.toString().padStart(5)} files | ${stats.lines.toString().padStart(8)} lines (${stats.percentage}%)`);
    });
    
    console.log('='.repeat(50));
};

/**
 * Analyze multiple directories and compare them
 */
const compareCodeBreakdowns = async (paths, options = {}) => {
    console.log(`🔄 Comparing code in ${paths.length} directories...`);
    
    const results = [];
    
    for (const srcPath of paths) {
        try {
            const breakdown = await codeBreakdown(srcPath, { ...options, showProgress: false });
            results.push({
                path: srcPath,
                breakdown
            });
        } catch (error) {
            console.warn(`⚠️ Failed to analyze ${srcPath}: ${error.message}`);
            results.push({
                path: srcPath,
                error: error.message
            });
        }
    }
    
    return results;
};

// Example usage (uncomment to test):
/*
(async () => {
    try {
        // Basic analysis of src folder
        const breakdown = await codeBreakdown('./src');
        printCodeBreakdown(breakdown);
        
        // Advanced analysis with custom options
        const advancedBreakdown = await codeBreakdown('./src', {
            excludeDirs: ['node_modules', 'dist', 'test'],
            includeExtensions: ['.js', '.ts', '.jsx', '.tsx'],
            verbose: true
        });
        
        // Compare multiple directories
        const comparison = await compareCodeBreakdowns([
            './src',
            './tests',
            './docs'
        ]);
        
        console.log('\n🔍 COMPARISON RESULTS:');
        comparison.forEach(result => {
            if (result.breakdown) {
                console.log(`${result.path}: ${result.breakdown.summary.linesOfCode} lines of code`);
            } else {
                console.log(`${result.path}: ${result.error}`);
            }
        });
        
    } catch (error) {
        console.error('❌ Analysis failed:', error.message);
    }
})();
*/
