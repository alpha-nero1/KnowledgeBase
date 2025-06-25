# 🐇 Projects / RabbitMQWithMassTransit
*⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🛠️ Project creation
sln: `dotnet new sln -n ActiveMQ`
api: `dotnet new web -n api`
worker: `dotnet new worker -n worker`
setup: `dotnet sln add api/api.csproj worker/worker.csproj`

## ✅ Spin up RabbitMQ locally
`docker run -d --name rabbit -p 5672:5672 -p 15672:15672 masstransit/rabbitmq`
