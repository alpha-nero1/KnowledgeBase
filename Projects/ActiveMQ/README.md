# 🎯 Projects / ActiveMQ
> [!WARNING]
> *The code in this project is NOT best practice; for demonstration purposes ONLY*

Demonstrates an ActiveMQ pub/sub application with net8.0.

## 🛠️ Project creation
sln: `dotnet new sln -n ActiveMQ`
api: `dotnet new web -n api`
worker: `dotnet new worker -n worker`
setup: `dotnet sln add api/api.csproj worker/worker.csproj`

## ✅ Start the ActiveMQ server locally with Docker:
```
# 61616 = OpenWire/JMS port
# 8161 = Web admin console
docker run -d --name activemq -p 61616:61616 -p 8161:8161 rmohr/activemq
```

## Important Libs
`Apache.NMS.ActiveMQ`