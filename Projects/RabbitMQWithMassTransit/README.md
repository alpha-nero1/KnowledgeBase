# 🐇 Projects / RabbitMQWithMassTransit
> [!WARNING]
> *The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🛠️ Project creation
sln: `dotnet new sln -n ActiveMQ`
api: `dotnet new web -n api`
worker: `dotnet new worker -n worker`
setup: `dotnet sln add api/api.csproj worker/worker.csproj`

## ✅ Spin up RabbitMQ locally
Broker listens on amqp://localhost:5672, Management UI at http://localhost:15672 (guest / guest)
`docker run -d --name rabbit -p 5672:5672 -p 15672:15672 masstransit/rabbitmq`

## ⚖️ Final Notes
This project was extremely easy to set up, MassTransit really does take care of alot of the heavy lifting for you with queues. I think about it like the Mediator of working with queues.

It was just so straight forward.