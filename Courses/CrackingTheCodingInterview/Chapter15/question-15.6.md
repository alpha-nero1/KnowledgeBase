# 15.6. Synchronised Methods
You are given a class with synchronized method A and a normal
method B. If you have two threads in one instance of a program, can they both execute A at the
same time? Can they execute A and B at the same time?

By applying the keyword `synchronized` to a method, we ensure that two threads cannot execute that method on the same instance object.

So if the thread is using the same instance then, A cannot be called by both. However different instances are fine.

Because no keyword was applied to B, any thread can call this method any time they like.