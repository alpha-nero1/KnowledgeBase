# Thread vs. Process: What's the difference between a thread and a process?
- process: An instance of a program in execution. Processes cannot access memory that another process is using.
- thread: Exists whithin a process. All threads within a process can access the same memory heap.
Each thread, however, still has it's own stack and registers.
