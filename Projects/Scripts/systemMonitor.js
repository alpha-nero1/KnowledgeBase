// =====================================
// 3. 📊 SYSTEM MONITORING & ANALYTICS
// =====================================

/**
 * Real-time system monitoring and performance analytics
 * Useful for: DevOps monitoring, performance tracking, alerts
 */
class SystemMonitor {
    constructor() {
        this.metrics = {};
        this.alerts = [];
        this.isMonitoring = false;
    }
    
    async start() {
        console.log('📊 Starting system monitoring...');
        this.isMonitoring = true;
        
        // Monitor every 5 seconds
        this.monitoringInterval = setInterval(() => {
            this.collectMetrics();
            this.checkAlerts();
        }, 5000);
        
        // Web dashboard
        this.startWebDashboard();
    }
    
    collectMetrics() {
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
        
        this.metrics[metrics.timestamp] = metrics;
        
        // Keep only last 100 entries
        const entries = Object.keys(this.metrics);
        if (entries.length > 100) {
            const toDelete = entries.slice(0, entries.length - 100);
            toDelete.forEach(key => delete this.metrics[key]);
        }
    }
    
    checkAlerts() {
        const latest = Object.values(this.metrics).slice(-1)[0];
        if (!latest) return;
        
        // Memory usage alert
        const memUsage = (latest.memory.usage.heapUsed / latest.memory.usage.heapTotal) * 100;
        if (memUsage > 80) {
            this.addAlert('HIGH_MEMORY_USAGE', `Memory usage: ${memUsage.toFixed(2)}%`);
        }
        
        // CPU load alert
        const cpuLoad = latest.cpu.loadAvg[0];
        if (cpuLoad > os.cpus().length * 0.8) {
            this.addAlert('HIGH_CPU_LOAD', `CPU load: ${cpuLoad.toFixed(2)}`);
        }
    }
    
    addAlert(type, message) {
        const alert = {
            type,
            message,
            timestamp: new Date().toISOString()
        };
        
        this.alerts.push(alert);
        console.warn(`🚨 ALERT [${type}]: ${message}`);
        
        // Keep only last 50 alerts
        if (this.alerts.length > 50) {
            this.alerts = this.alerts.slice(-50);
        }
    }
    
    startWebDashboard() {
        const server = http.createServer((req, res) => {
            res.setHeader('Content-Type', 'application/json');
            res.setHeader('Access-Control-Allow-Origin', '*');
            
            const data = {
                metrics: this.metrics,
                alerts: this.alerts,
                status: this.isMonitoring ? 'active' : 'stopped'
            };
            
            res.end(JSON.stringify(data, null, 2));
        });
        
        server.listen(3001, () => {
            console.log('📊 Monitoring dashboard available at http://localhost:3001');
        });
    }
    
    stop() {
        console.log('🛑 Stopping system monitoring...');
        this.isMonitoring = false;
        if (this.monitoringInterval) {
            clearInterval(this.monitoringInterval);
        }
    }
}