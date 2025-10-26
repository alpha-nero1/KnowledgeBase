# Context Switch: How would you measure the time spent in a context switch? 
A context switch is the time switching between two processes. This can happen in mutlitasking.
&nbsp;
The OS must bring the state information of waiting processes into memory and save the state info of the currently running process.
(retreive timestamp of waiting process and save the timestamp of current)
&nbsp;
Solving this requires two main steps:
1. Record the timestamps of the last and first instruction of the swapping processes.
2. Calculate context switch time by seeing the timestamp difference from last task of 1 and first of 2.
&nbsp;
The tricky part is that we cannot record the timestamp of each instruction so how can we know when the swapping occurs?
Another issue is that these processes also may be competing with other resources using the CPU so at time of swap if something else is happing this can overstate the switch time.
&nbsp;
So in order to solve we must construct an environment where after p1 executes the task scheduler immediately selects p2 to run. We can accomplish this by constructing a "data channel" such as a pipe between p1 and p2. and have the two processes play ping pong with the data token.
&nbsp;
This means that we allow P1 to be the initial sender and P2 to be the receiver. Initially p2 is blocked as it awaits the data token. When p1 executes it delivers the token to p2 (over the data channel) and p1 immediately tries to read a response token. However since p2 has not yet made it into a running state no token is available for p1 and the process is blocked. This relinquishes the CPU. A context switch results and the task scheduler must select another process to run (p2) since it is now in a ready to run state

"
To summarize, an iteration of the game is played with the following steps: 
1. P 2 blocks awaiting data from P 1
2. P1 marks the start time. 
3. P1 sends token to P 2
4. P1 attempts to read a response token from P 2. This induces a context switch.
5. P2 is scheduled and receives the token. 
6. P2 sends a response token to P 1
7. P2 attempts read a response token from P 1. This induces a context switch.
8. P 1 is scheduled and receives the token.
9. P1 marks the end time. 
"