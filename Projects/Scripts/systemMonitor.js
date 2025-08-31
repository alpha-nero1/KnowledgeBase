/**
 * Real-time system monitoring and performance analytics.
 * Useful for: DevOps monitoring, performance tracking, alerts
 */

import { createServer } from 'http';
import process from 'process';
import os from 'os';

/**
 * Creates a metrics collector with configurable retention
 */
const createMetricsCollector = (maxEntries = 100) => {
    // Private state captured in closure
    const state = {
        metrics: {},
        maxEntries
    };

    const collectMetrics = () => {
        const metrics = {
            timestamp: new Date().toISOString(),
            cpu: {
                usage: process.cpuUsage(),
                loadAvg: os.loadavg()
            },
            memory: {
                usage: process.memoryUsage(),
                system: {
                    total: os.totalmem(),
                    free: os.freemem()
                }
            },
            uptime: process.uptime(),
            platform: os.platform(),
            arch: os.arch()
        };
        
        state.metrics[metrics.timestamp] = metrics;
        
        // Keep only last maxEntries
        const entries = Object.keys(state.metrics);
        if (entries.length > state.maxEntries) {
            const toDelete = entries.slice(0, entries.length - state.maxEntries);
            toDelete.forEach(key => delete state.metrics[key]);
        }

        return metrics;
    };

    const getMetrics = () => ({ ...state.metrics });
    const getLatestMetric = () => Object.values(state.metrics).slice(-1)[0];
    const clearMetrics = () => { state.metrics = {}; };

    return {
        collectMetrics,
        getMetrics,
        getLatestMetric,
        clearMetrics
    };
};

/**
 * Creates an alert manager with configurable thresholds
 */
const createAlertManager = (thresholds = {}, maxAlerts = 50) => {
    // Default thresholds
    const defaultThresholds = {
        memoryUsage: 80,
        cpuLoadMultiplier: 0.8,
        ...thresholds
    };

    // Private state
    const state = {
        alerts: [],
        thresholds: defaultThresholds,
        maxAlerts
    };

    const addAlert = (type, message) => {
        const alert = {
            type,
            message,
            timestamp: new Date().toISOString()
        };
        
        state.alerts.push(alert);
        console.warn(`🚨 ALERT [${type}]: ${message}`);
        
        // Keep only last maxAlerts
        if (state.alerts.length > state.maxAlerts) {
            state.alerts = state.alerts.slice(-state.maxAlerts);
        }

        return alert;
    };

    const checkAlerts = (metrics) => {
        if (!metrics) return [];

        const newAlerts = [];

        // Memory usage alert
        const memUsage = (metrics.memory.usage.heapUsed / metrics.memory.usage.heapTotal) * 100;
        if (memUsage > state.thresholds.memoryUsage) {
            newAlerts.push(addAlert('HIGH_MEMORY_USAGE', `Memory usage: ${memUsage.toFixed(2)}%`));
        }
        
        // CPU load alert
        const cpuLoad = metrics.cpu.loadAvg[0];
        if (cpuLoad > os.cpus().length * state.thresholds.cpuLoadMultiplier) {
            newAlerts.push(addAlert('HIGH_CPU_LOAD', `CPU load: ${cpuLoad.toFixed(2)}`));
        }

        return newAlerts;
    };

    const getAlerts = () => [...state.alerts];
    const clearAlerts = () => { state.alerts = []; };
    const updateThresholds = (newThresholds) => {
        Object.assign(state.thresholds, newThresholds);
    };

    return {
        addAlert,
        checkAlerts,
        getAlerts,
        clearAlerts,
        updateThresholds,
        getThresholds: () => ({ ...state.thresholds })
    };
};

/**
 * Creates a web dashboard server
 * @param {number} port - Port to run the dashboard on
 * @returns {Object} Dashboard control functions
 */
const createWebDashboard = (port = 3001) => {
    let server = null;

    const start = (getDataCallback) => {
        server = createServer((req, res) => {
            res.setHeader('Content-Type', 'application/json');
            res.setHeader('Access-Control-Allow-Origin', '*');
            
            const data = getDataCallback();
            res.end(JSON.stringify(data, null, 2));
        });
        
        server.listen(port, () => {
            console.log(`📊 Monitoring dashboard available at http://localhost:${port}`);
        });

        return server;
    };

    const stop = () => {
        if (server) {
            server.close();
            server = null;
        }
    };

    return {
        start,
        stop,
        isRunning: () => server !== null
    };
};

/**
 * Factory function that creates a complete system monitor.
 */
const SystemMonitor = (config = {}) => {
    const {
        metricsRetention = 100,
        maxAlerts = 50,
        monitoringInterval = 5000,
        dashboardPort = 3001,
        alertThresholds = {}
    } = config;

    // Create component instances
    const metricsCollector = createMetricsCollector(metricsRetention);
    const alertManager = createAlertManager(alertThresholds, maxAlerts);
    const webDashboard = createWebDashboard(dashboardPort);

    // Private state for monitoring control
    const state = {
        isMonitoring: false,
        monitoringInterval: null
    };

    // Private monitoring loop
    const monitoringLoop = () => {
        const latestMetrics = metricsCollector.collectMetrics();
        alertManager.checkAlerts(latestMetrics);
    };

    const start = async () => {
        console.log('📊 Starting system monitoring...');
        state.isMonitoring = true;
        
        // Start monitoring loop
        state.monitoringInterval = setInterval(monitoringLoop, monitoringInterval);
        
        // Start web dashboard with data provider
        webDashboard.start(() => ({
            metrics: metricsCollector.getMetrics(),
            alerts: alertManager.getAlerts(),
            status: state.isMonitoring ? 'active' : 'stopped'
        }));

        return {
            isMonitoring: state.isMonitoring,
            dashboardPort
        };
    };

    const stop = () => {
        console.log('🛑 Stopping system monitoring...');
        state.isMonitoring = false;
        
        if (state.monitoringInterval) {
            clearInterval(state.monitoringInterval);
            state.monitoringInterval = null;
        }
        
        webDashboard.stop();
    };

    // Manual operations
    const collectMetricsNow = () => metricsCollector.collectMetrics();
    const checkAlertsNow = () => {
        const latest = metricsCollector.getLatestMetric();
        return alertManager.checkAlerts(latest);
    };

    return {
        // Core monitoring controls
        start,
        stop,
        
        // Manual operations
        collectMetricsNow,
        checkAlertsNow,
        
        // Data access
        getMetrics: metricsCollector.getMetrics,
        getAlerts: alertManager.getAlerts,
        getLatestMetric: metricsCollector.getLatestMetric,
        
        // Configuration
        updateAlertThresholds: alertManager.updateThresholds,
        getAlertThresholds: alertManager.getThresholds,
        
        // Status
        isMonitoring: () => state.isMonitoring,
        isDashboardRunning: webDashboard.isRunning,
        
        // Utilities
        clearMetrics: metricsCollector.clearMetrics,
        clearAlerts: alertManager.clearAlerts
    };
};

// Auto-start the monitor when script runs
const monitor = SystemMonitor();

// Graceful shutdown handler
const gracefulShutdown = () => {
    console.log('\n🛑 Received shutdown signal...');
    monitor.stop();
    console.log('✅ System monitor stopped gracefully');
    process.exit(0);
};

// Handle various shutdown signals
process.on('SIGINT', gracefulShutdown);  // Ctrl+C
process.on('SIGTERM', gracefulShutdown); // Termination signal
process.on('SIGQUIT', gracefulShutdown); // Quit signal

// Handle uncaught exceptions
process.on('uncaughtException', (error) => {
    console.error('💥 Uncaught Exception:', error);
    monitor.stop();
    process.exit(1);
});

// Handle unhandled promise rejections
process.on('unhandledRejection', (reason, promise) => {
    console.error('💥 Unhandled Rejection at:', promise, 'reason:', reason);
    monitor.stop();
    process.exit(1);
});

// Start the monitor
monitor.start().then(() => {
    console.log('🚀 System monitor is running...');
    console.log('📊 Dashboard: http://localhost:3001');
    console.log('⌨️  Press Ctrl+C to stop');
}).catch((error) => {
    console.error('❌ Failed to start monitor:', error);
    process.exit(1);
});
