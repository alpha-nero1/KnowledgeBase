9.5 Cache - Imagine a webserver of 100 machines facilitating a search engine... how would you effectively implement a caching mechanism for searches.

Effectively we need to start with a caching mechanism that is very similar to a hash table, however, it must implement the linked lists solution under the hood where it keeps multiple values in a key that are sorted by most recent.

In order to effectively store and retreive the hashtable across the machines we can effectively create a map that maps to each machine. This means that any machine could figure out which machine stores or where to get a segment of the cache from by implementing:

```
correct_mach = hash(query) % N
```

Where N is the number of machines in the network.