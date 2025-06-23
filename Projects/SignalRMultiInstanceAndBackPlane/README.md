# 📨 Projects / SignalRMultiInstanceAndBackplane
Demonstrates a scalable solution with SignalR.

In this project we have multiple users (different tabs with a new instance of the client open), who can send messages to other specific users, connecting through the hub set up in the **api**.

But behind the scenes it is leveraging the redis stack exchange (for multiple instances of the server running in parrallel) 
and includes the backplane for use by a publisher.

The project demonstrates the following:
1. How to use SignalR
2. How to do auth with SignalR
3. How to run multiple instances of our hub
4. How to implement a backplane for sending messages from a producer to subscriber (the hub)

## 🛠️ Project creation
`dotnet new web -n api`
`npm create vite@latest client -- --template react`

## 🏎️ How to run 
### 🖥️ Client
```
cd client      
npm install    
npm run dev
```

### 🌐 Api
```
cd api
dotnet restore
dotnet build
dotnet run
```

## Notes
* Backend uses `Microsoft.AspNetCore.SignalR` library. `dotnet add package Microsoft.AspNetCore.SignalR`
