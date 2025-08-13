// =====================================
// 5. 🌐 WEB SCRAPING & API AUTOMATION
// =====================================

/**
 * Web scraping and API integration automation
 * Useful for: Data collection, API testing, content aggregation
 */
class WebAutomation {
    constructor() {
        this.rateLimiter = new Map(); // Simple rate limiting
    }
    
    async fetchWithRetry(url, options = {}, maxRetries = 3) {
        const requestFn = url.startsWith('https:') ? https.get : http.get;
        
        for (let attempt = 1; attempt <= maxRetries; attempt++) {
            try {
                console.log(`🌐 Fetching: ${url} (attempt ${attempt})`);
                
                const data = await new Promise((resolve, reject) => {
                    const req = requestFn(url, options, (res) => {
                        let data = '';
                        res.on('data', chunk => data += chunk);
                        res.on('end', () => {
                            if (res.statusCode >= 200 && res.statusCode < 300) {
                                resolve(data);
                            } else {
                                reject(new Error(`HTTP ${res.statusCode}: ${res.statusMessage}`));
                            }
                        });
                    });
                    
                    req.on('error', reject);
                    req.setTimeout(10000, () => {
                        req.destroy();
                        reject(new Error('Request timeout'));
                    });
                });
                
                console.log(`✅ Successfully fetched: ${url}`);
                return data;
                
            } catch (error) {
                console.warn(`⚠️ Attempt ${attempt} failed: ${error.message}`);
                
                if (attempt === maxRetries) {
                    console.error(`❌ Failed to fetch after ${maxRetries} attempts: ${url}`);
                    throw error;
                }
                
                // Exponential backoff
                await this.sleep(Math.pow(2, attempt) * 1000);
            }
        }
    }
    
    /**
     * Batch process multiple URLs with rate limiting
     */
    async batchProcess(urls, processor, concurrency = 5) {
        console.log(`🔄 Batch processing ${urls.length} URLs...`);
        
        const results = [];
        const inProgress = new Set();
        
        for (let i = 0; i < urls.length; i++) {
            const url = urls[i];
            
            // Wait if we've hit concurrency limit
            while (inProgress.size >= concurrency) {
                await this.sleep(100);
            }
            
            const promise = (async () => {
                try {
                    const result = await processor(url);
                    results[i] = { url, result, status: 'success' };
                } catch (error) {
                    results[i] = { url, error: error.message, status: 'error' };
                } finally {
                    inProgress.delete(promise);
                }
            })();
            
            inProgress.add(promise);
        }
        
        // Wait for all remaining requests
        await Promise.all(inProgress);
        
        console.log(`✅ Batch processing complete. Processed ${results.length} URLs`);
        return results;
    }
    
    sleep(ms) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
}