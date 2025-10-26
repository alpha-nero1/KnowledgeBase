# Java

## Private constructor
"In terms of inheritance, what is the effect of keeping a constructor private?"

The effect of keeping a constructor private is that only inner classes of 'A' can access the constructor (nested classes). Also, if A is a nested class of B then B's other nested classes can access A's private constructor.

## Return from finally
"In Java, does the finally block get executed if we insert a return statement 
inside the try block of a try-catch-finally?"

Yes the finally block will still be executed because the finally block is only waiting for the try-catch blocks to exit (return will only end those inner blocks)

Literally the only way that finally will not be called is if the thread gets killed ot the machine exits during the try-catch.

## Final e.t.c
"What is the difference between final, finally, and finalize? "

- `final`: when applied to a variable, the value can no longer change
- `finally`: executes a block after a try catch block.
- `finalize`: The AGC (Automatic Garbage Collector) calls this function on an object right before it is collected, this way we can override it and do special things in there like:

```
protected void finalize() throws Throwable {
    // Close files, release resources, e.t.c.
}
```