9.7 Personal Financial Manager: Explain how you would design a personal financial manager (like 
Mint.com). This system would connect to your bank accounts, analyze your spending habits, and 
make recommendations.

1. Scope the problem
2. Make reasonable assumptions
3. Draw out the main components

There will be key system components.

1. Data synchroniser service
This will listen for updates from the bank and be able to query past transactions.

Each new update it gets will add a sychronisation task to the queue with given priorities attached to each task.

2. Data processing queue
The main bulk of the program which will continually process a queue of tasks, most important are processed first, while lower priority ones are starved. We can have as many replicas of the processing queue service as we like if we need to scale up and down.

at the end of each task (that syncs and stores the data in our own DB) we will make a call to the budget service.

3. Budget service
The budget service accepts the new transaction and compares it to what totals are already stored for the categories that the user is tracking. If that amount pushes the user over budjet, an email should be sent out.

4. Web app
Shows data for your given account.