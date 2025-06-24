# 📨 Projects / SignalR
*⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

Demonstrates how a very simple websocket setup can work with dotnet and react.

In this project we have multiple users (different tabs with a new instance of the client open), who can send messages to other specific users, connecting through the hub set up in the **api**.

The project demonstrates the following:
1. How to use SignalR
2. How to do auth with SignalR

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

Chat GPT link: https://chatgpt.com/share/6853debb-4a34-8005-b2cb-70fe1b92a086