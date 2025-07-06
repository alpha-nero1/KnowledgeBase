# 🎆 Projects / Kafka
> *⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🤷‍♂️ What does it do?
This project demonstrates a simple Apache Kafka and Zookeper setup.

### What is Apache Kafka
Kafka is a distributed log-based system as opposed to a typical message broker. It is ideal for:
- High throughput
- Persistent logs
- Stream processing
- Replayable events

Kafka is great for event-driven architectures, stream processing, and analytics. Think: data pipelines, event sourcing, microservices with replayable events.

> ℹ️ **Apache Kafka** is like SignalR and Redis on ROIDS. They do similar things (pub/sub/stream events) but Kafka has the upper hand in that it's messages are **Durable** (can retain for days or weeks)

### 🧠 Use cases
- Scalable pub-sub system for delivering messages to many users.
- Ingest and analyze telemetry from millions of devices.
- Track media playback, buffering, quality-of-service (QoS) in real time.
- Move data between systems in real-time (or batch), e.g. DB → Kafka → Data Warehouse
- Store all changes to data in an immutable, replayable log.
- Process high-throughput logs, metrics, or user activity in real time.
- Send domain events between services without tight coupling.

## 🛠️ Project setup
sln: `dotnet new sln -n Kafka`
api: `dotnet new web -n api`
worker: `dotnet new worker -n worker`
setup: `dotnet sln add api/api.csproj worker/worker.csproj`

## 🏎️ How to run 
Run Kafka: `docker-compose up -d`

## ⚖️ Final Remarks
