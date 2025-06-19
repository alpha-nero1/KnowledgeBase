# Threads and Locks
- Every thread in Java is handled by an instance of the `java.lang.Thread` object. in C# this would be `Task` (using `System.Threading.Tasks`)

## Lock
- A lock: A lock in terms of threads can be described as two seperate running threads trying to access a given
resource at the same time, this can cause a lock because the memory is shared.

## Synchronisation
- synchronization: "Most commonly, we restrict access to shared resources through the use of the synchronized keyword. It 
can be applied to methods and code blocks, and restricts multiple threads from executing the code simultaneously on the same object. "

"For more granular control, we can utilize a lock. A lock (or monitor) is used to synchronize access to a 
shared resource by associating the resource with the lock. A thread gets access to a shared resource by first 
acquiring the lock associated with the resource. At any given time, at most one thread can hold the lock 
and, therefore, only one thread can access the shared resource."

## Context Switch
A context switch represents the switching from one thread to another.

"A context switch is the time spent switching between two processes (i.e., bringing a waiting process into 
execution and sending an executing process into waiting/terminated state). This happens in multitasking. 
The operating system must bring the state information of waiting processes into memory and save the 
state information of the currently running process."