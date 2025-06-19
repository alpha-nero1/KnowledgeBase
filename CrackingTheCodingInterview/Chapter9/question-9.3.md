## Web crawler
To build a web crawler without running into crawling loops what we can do is have an in memory mapping
where a crawler is given an id and the id maps to a set of urls.

Each time the crawaler crawls, if it runs into a url in the set, it can terminate itself.

e.g.

```
const crawlerMap = new Map(['crawlr-bot-alpha', new Set()]);

const crawl = (botid, url) => {
    ... get the document and parse urls in it ...
    if (crawlerMap.get(botid).has(url)) return url;
    urls.forEach((url) => crawl(botid, url))
}

crawl('crawlr-bot-alpha', 'www.coolguysite.com')
```
