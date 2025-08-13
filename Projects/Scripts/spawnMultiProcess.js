const { spawn } = require('child_process');

// ANSI Color codes for terminal output
const colors = {
    reset: '\x1b[0m',
    bright: '\x1b[1m',
    dim: '\x1b[2m',
    red: '\x1b[31m',
    green: '\x1b[32m',
    yellow: '\x1b[33m',
    blue: '\x1b[34m',
    magenta: '\x1b[35m',
    cyan: '\x1b[36m',
    white: '\x1b[37m',
    bgRed: '\x1b[41m',
    bgGreen: '\x1b[42m',
    bgYellow: '\x1b[43m',
    bgBlue: '\x1b[44m',
    bgMagenta: '\x1b[45m',
    bgCyan: '\x1b[46m'
};

const commandFactory = (name, cwd, cmd, args, colour = 'white') => ({
    name,
    cwd,
    cmd,
    args,
    colour
});

const colorText = (text, color) => `${colors[color] || colors.white}${text}${colors.reset}`;

const logInfo = (name, colour, output) => {
    const coloredName = colorText(`[${name}]`, colour);
    console.log(`${coloredName} ${output}`);
}

const logError = (name, colour, output) => {
    const coloredName = colorText(`[${name}]`, colour);
    console.error(`${coloredName} ${output}`);
}

/**
 * Entry point function, spawn processes in parrallel!
 */
const spawnProcesses = async (...commands) => {
    console.log('🔀 Starting multi-process orchestration...');
    
    const processes = [];
    
    for (const { name, cwd, cmd, args, colour } of commands) {
        const process = spawn(cmd, args, {
            cwd,
            stdio: ['inherit', 'pipe', 'pipe']
        });
        
        process.stdout?.on('data', (data) => {
            const output = data.toString().trim();
            logInfo(name, colour, output);
        });
        
        process.stderr?.on('data', (data) => {
            const output = data.toString().trim();
            const lowerOutput = output.toLower();
            if (lowerOutput.contains(/error|exception/)) {
                logError(name, colour, output);
            } else {
                logInfo(name, colour, output);
            }
        });
        
        processes.push({ name, process });
    }
    
    // Graceful shutdown handler
    process.on('SIGINT', () => {
        console.log('\n🛑 Shutting down all processes...');
        processes.forEach(({ name, process }) => {
            console.log(`Terminating ${name}...`);
            process.kill('SIGTERM');
        });
        process.exit(0);
    });
    
    return processes;
};

// Example usage:
spawnProcesses(
    commandFactory('AppOne', './AppOne', 'npm', ['start'], colors.blue),
    commandFactory('AppTwo', './AppTwo', 'npm', ['start'], colors.green),
    commandFactory('AppThree', './AppThree', 'npm', ['start'], colors.yellow)
)