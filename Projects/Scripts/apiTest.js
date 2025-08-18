/**
 * ApiAutomation (apiTest.js) is a common example of how to write an API test script in JavaScript
 * featuring many goodies such as retries, rate limiting, timeouts, batching and confuguration,
 * it's pretty neat!
 */

const Methods = {
    GET: 'GET',
    POST: 'POST',
    PUT: 'PUT',
    PATCH: 'PATCH',
    DELETE: 'DELETE'
}
const sleep = (ms) => new Promise(resolve => setTimeout(resolve, ms));
// HTTP request builder helpers
const httpRequest = (method, url, body = null, headers = {}) => ({
    method, url, body, headers
});

// Utilities, not necessary to use through.
const GET = (url, headers = {}) => httpRequest(Methods.GET, url, null, headers);
const POST = (url, body, headers = {}) => httpRequest(Methods.POST, url, body, headers);
const PUT = (url, body, headers = {}) => httpRequest(Methods.PUT, url, body, headers);
const PATCH = (url, body, headers = {}) => httpRequest(Methods.PATCH, url, body, headers);
const DELETE = (url, headers = {}) => httpRequest(Methods.DELETE, url, null, headers);
const RequestsWithBody = new Set([Methods.POST, Methods.PUT, Methods.PATCH])
const timeoutMs = 10000;

/**
 * Web scraping and API integration automation
 * Useful for: Data collection, API testing, content aggregation
 */
const ApiAutomation = () => {
    const rateLimitMap = new Map();
    
    const checkRateLimit = async (domain, requestsPerSecond = 2) => {
        const now = Date.now();
        const domainData = rateLimitMap.get(domain) || { requests: [], lastCleanup: now };
        
        // Clean up old requests (older than 1 second)
        domainData.requests = domainData.requests.filter(timestamp => now - timestamp < 1000);
        
        // Check if we're within rate limit
        if (domainData.requests.length >= requestsPerSecond) {
            const oldestRequest = Math.min(...domainData.requests);
            const waitTime = 1000 - (now - oldestRequest);
            console.log(`⏱️  Rate limiting: waiting ${waitTime}ms for ${domain}`);
            await sleep(waitTime);
            return checkRateLimit(domain, requestsPerSecond); // Recursive check after waiting
        }
        
        // Add current request timestamp
        domainData.requests.push(now);
        rateLimitMap.set(domain, domainData);
    };
    
    /**
     * Pretty heavy fetch method handling both retires and rate limiting.
     */
    const fetchWithRetry = async ({ method = Methods.GET, url, body, headers = {} }, maxRetries = 3) => {
        const domain = new URL(url).hostname;
        await checkRateLimit(domain);
        
        // Build fetch options
        const fetchOptions = {
            method: method.toUpperCase(),
            headers: {
                'User-Agent': 'ApiAutomation/1.0',
                ...headers
            }
        };
        
        // Add body for requests that support it!
        if (RequestsWithBody.has(method) && body) {
            fetchOptions.body = typeof body === 'string' ? body : JSON.stringify(body);
            if (!fetchOptions.headers['Content-Type']) {
                fetchOptions.headers['Content-Type'] = 'application/json';
            }
        }
        
        for (let attempt = 1; attempt <= maxRetries; attempt++) {
            try {
                console.log(`🌐 ${method.toUpperCase()} ${url} (attempt ${attempt})`);
                
                const controller = new AbortController();
                const timeoutId = setTimeout(() => controller.abort(), timeoutMs);
                
                const response = await fetch(url, {
                    ...fetchOptions,
                    signal: controller.signal
                });
                
                clearTimeout(timeoutId); // Clear timeout if request succeeds
                
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                
                const data = await response.text();
                console.log(`✔️  Successful ${method.toUpperCase()} ${url}`);
                return data;
                
            } catch (error) {
                // Handle different error types
                let errorMessage = error.message;
                if (error.name === 'AbortError') {
                    errorMessage = `Request timeout (${timeoutMs / 1000}s)`;
                }
                
                console.warn(`⚠️ Attempt ${attempt} failed: ${errorMessage}`);
                
                if (attempt === maxRetries) {
                    console.error(`❌ Failed to fetch after ${maxRetries} attempts: ${url}`);
                    throw new Error(errorMessage);
                }
                
                // Exponential backoff
                await sleep(Math.pow(2, attempt) * 1000);
            }
        }
    };
    
    /**
     * Batch process multiple URLs with rate limiting
     * Uses fetchWithRetry by default, but allows custom processor
     */
    const batchProcess = async (httpRequests, processor = null, concurrency = 5) => {
        console.log(`🔄 Batch processing ${httpRequests.length} requests...`);
        
        // Use fetchWithRetry as default processor if none provided
        const actualProcessor = processor || ((req) => fetchWithRetry(req));
        
        const results = [];
        const inProgress = new Set();
        
        for (let i = 0; i < httpRequests.length; i++) {
            const request = httpRequests[i];
            const { url, method } = request;
            
            // If we are already processing the limit (5) then keep
            // waiting to proceed.
            while (inProgress.size >= concurrency) {
                await sleep(100);
            }
            
            // This is the funky bit, returns the promise and it has also been invoked already.
            // This allows us to fire the api call and store the promise
            // in the inProgress Set() to track for future iterations.
            // Inside the promise it deletes itself from inProgress.
            // Jecking crazy use of JavaScript.
            const promise = (async () => {
                try {
                    const result = await actualProcessor(request);
                    results[i] = { method, url, result, status: 'success' };
                } catch (error) {
                    results[i] = { method, url, error: error.message, status: 'error' };
                } finally {
                    inProgress.delete(promise);
                }
            })();
            
            inProgress.add(promise);
        }
        
        // Wait for all remaining requests
        await Promise.all(inProgress);
        
        console.log(`✔️  Batch processing complete. Processed ${results.length} requests`);
        return results;
    };

    const batchProcessGet = (urls, processor = null, concurrency = 5) => 
        batchProcess(urls.map(url => GET(url)), processor, concurrency)
    
    // Get rate limiting stats
    const getRateLimitStats = () => {
        const stats = {};
        for (const [domain, data] of rateLimitMap.entries()) {
            stats[domain] = {
                activeRequests: data.requests.length,
                lastRequest: data.requests.length > 0 ? new Date(Math.max(...data.requests)) : null
            };
        }
        return stats;
    };
    
    // Clear rate limiting data for a domain or all domains
    const clearRateLimit = (domain = null) => {
        if (domain) {
            rateLimitMap.delete(domain);
            console.log(`🧹 Cleared rate limit data for ${domain}`);
        } else {
            rateLimitMap.clear();
            console.log(`🧹 Cleared all rate limit data`);
        }
    };
    
    return {
        fetchWithRetry,
        batchProcess,
        // Utility wrapping batch process making it super easy to test a bunch
        // of GET requests.
        batchProcessGet,
        getRateLimitStats,
        clearRateLimit
    };
};


// Usage Examples:
const automation = ApiAutomation();

// Simple GET requests using the utility wrapper
const getResults = await automation.batchProcessGet([
    'http://google.com',
    'http://google.com',
    'http://google.com'
]);

console.log('✅ Results: ')
console.log(getResults.map(res => ({ ...res, result: 'hidden' })));

// Custom processor example: 
// const customProcessor = async (request) => {
//     console.log(`Custom processing: ${request.method} ${request.url}`);
//     const response = await automation.fetchWithRetry(request);
//     // Parse JSON if needed
//     try {
//         return JSON.parse(response);
//     } catch (e) {
//         return response; // Return raw response if not JSON
//     }
// };

// const customResults = await automation.batchProcessGet([
//     GET('https://api.example.com/data'),
//     POST('https://api.example.com/data', { key: 'value' })
// ], customProcessor);