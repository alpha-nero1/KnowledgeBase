# 13.5. TreeMap, HashMap, LinkedHashMap
Explain the differences between TreeMap, HashMap, and
LinkedHashMap. Provide an example of when each one would be best.

## HashMap
Offers the standard hashtable implementation, implemented by an array of linked lists, it provides access and insertion of Log(1). When iterating keys, the ordering is not guaranteed and is at random.

## TreeMap
Keys are ordered with TreeMaps, not by time of insertion, but by a comparison (e.g. you could do alphabetical). Keys must implement the `Comparable` interface.

## LinkedHashMap
This hashtable solution keeps track of order of insertion, implemented by doubly-linked buckets. E.g. a stack or linked list is maintained that maps the key with where to access it in the array.