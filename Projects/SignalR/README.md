# 📨 Projects / SignalR
Demonstrates how a very simple websocket setup can work with dotnet and react.

In this project we have multiple users (different tabs with a new instance of the client open), who can send messages to other specific users, connecting through the hub set up in the **api**.

We also explore the concept of a **SignalR backplane**.

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

## 🔨 What is left to do?
* Send messages between specific users only instead of broadcast.
* Client "logs in" defining it's username and can send messages through the hub to others.
* Backplane?

Chat GPT link: https://chatgpt.com/share/6853debb-4a34-8005-b2cb-70fe1b92a086