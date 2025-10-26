9.4 Duplicate URLs: You have 10 billion URLs. How do you detect the duplicate documents? In this 
case, assume "duplicate" means that the URLs are identical.

- If each url is on avergae 100 chars and each char is 4 bytes then this storage would be 4 terabytes.

# Solution
If we had all the data on one machine we could do two passes of the document.

The first pass would split the document into smaller documents, we can create 4000 files each 1GB in size.

We can do this by looping all urls and storing them in files such as:
<x>.txt = hash(u) % 4000

We can then load each individual file in memory maintaining a hash table to remove or detect duplicates.