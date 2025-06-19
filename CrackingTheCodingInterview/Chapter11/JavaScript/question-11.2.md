# Random Crashes.
"You are given the source to an application which crashes when it is run. After 
running it ten times in a debugger, you find it never crashes in the same place. The application is 
single threaded, and uses only the C standard library. What programming errors could be causing 
this crash? How would you test each one? "

There are several things that could be causing such a crash.
1. `Random Variable`: The application may be using a random variable component that is not fixed for each run of the application.
2. `Different Environment`: Your machine could be interfering with the environment the app is meant to be runnining on (using a corrupted local library instead of a packaged one). this is why docker containers are so vital.
3. `Memory Leak`: The program may have a memory leak that builds up on start time and depends on what amount of processes are running at start time. Debug the memeory size as the app boots up.
4. `External Dependencies`: Perhaps the app relies on several external APIs to boot up at different parts of the start up.
5. `Uninitialised Variables`: the app could have an uninitialised variable which may cause it to take an arbitrary (almost random value). This could cause different paths of code execution on startup.