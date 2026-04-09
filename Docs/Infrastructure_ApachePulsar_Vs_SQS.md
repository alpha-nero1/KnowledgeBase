# Apache Pulsar vs Amazon SQS

## Quick Summary
- Use **Amazon SQS** when you want a fully managed, simple, highly reliable cloud queue with minimal operational overhead.
- Use **Apache Pulsar** when you need event-streaming features, multiple subscription models, replay, stronger ordering control, and deep customization.

## What Event Streaming Is
Event streaming is a way to continuously capture and process events (for example: user clicks, payments, sensor updates, order status changes) as they happen.

Key ideas:
- **Events are immutable records**: each event describes something that happened at a point in time.
- **Producers write, consumers read**: many services can independently consume the same stream.
- **Retention and replay**: events are kept for a period of time so consumers can re-read history.
- **Real-time + historical use**: the same stream can power live processing and later analytics.

In short: queueing is usually about "do this task," while event streaming is often about "this happened; multiple systems may react now or later."

## Main Differences

| Area | Apache Pulsar | Amazon SQS |
|---|---|---|
| Service model | Open-source distributed messaging and streaming platform (self-managed or managed via vendors) | Fully managed AWS queue service |
| Primary use case | Event streaming + messaging with long retention and replay | Task queues and decoupled asynchronous processing |
| Message retention | Configurable long retention on topics; replay is a core feature | Retention up to 14 days; replay model is more limited than stream platforms |
| Consumer model | Multiple subscriptions (exclusive, shared, failover, key-shared), pub/sub semantics | Queue consumers pull messages; Standard or FIFO queue types |
| Ordering | Strong ordering options (for partitions/keys depending on setup) | Standard: best-effort ordering; FIFO: strict order with throughput limits |
| Throughput scaling | High throughput via partitions and cluster scaling | Scales very well automatically; limits depend on queue type and API usage |
| Routing/filters | Rich topic + subscription patterns and ecosystem connectors | Basic queue patterns; filtering/routing often done with SNS + SQS |
| Multi-tenancy | Built-in tenant/namespace model | Account/region/service boundary model inside AWS |
| Ecosystem fit | Strong for poly-cloud or self-hosted streaming architectures | Best fit in AWS-native architectures |
| Operations | Requires platform expertise (unless using managed Pulsar) | Almost no infrastructure operations |

## Pros and Cons

### Apache Pulsar
Pros:
- Supports both queue-like and stream-like workloads.
- Built-in replay and long-lived event retention.
- Flexible subscription models for different consumption patterns.
- Good for multi-tenant platforms and cross-environment architectures.
- Open ecosystem and avoids lock-in to a single cloud vendor.

Cons:
- More architecture and operational complexity than managed queues.
- Requires deeper tuning, monitoring, and capacity planning.
- Team needs messaging platform expertise.
- Managed offerings may reduce complexity but add vendor/platform choices.

### Amazon SQS
Pros:
- Very easy to adopt and operate.
- Highly durable and scalable out of the box.
- Tight integration with AWS services (Lambda, SNS, EventBridge, IAM, CloudWatch).
- Pay-as-you-go with no server management.

Cons:
- Primarily a queue, not a full event-streaming platform.
- Advanced routing and fan-out usually require additional AWS services.
- Replay and long-term event history are limited compared with streaming systems.
- Strong ordering needs FIFO, which can impose throughput and design constraints.

## Practical Decision Guide
- Choose **SQS** if your main goal is reliable async job processing in AWS with minimal ops.
- Choose **Pulsar** if your main goal is streaming architecture, replayable events, and advanced consumer patterns.
- If your workload is simple today but may evolve into event streaming, evaluate whether starting with SQS + SNS is enough or if Pulsar will reduce future re-architecture.

## Real-World Examples (Publicly Referenced)

### Apache Pulsar
- **Yahoo**: Pulsar originated at Yahoo and was run in production at large scale for pub/sub messaging.
- **Tencent**: reports very large-scale real-time data transmission for game and analytics workloads using Pulsar in its data platform.
- **Splunk**: references Pulsar for durable, low-latency stream processing and routing scenarios.
- **Cisco (IoT Control Center)**: shared how Pulsar replaced legacy messaging in a large multi-datacenter IoT platform transformation.
- **Discord**: engineering story references Pulsar in a real-time ML and streaming stack (with Flink/Iceberg).

### Amazon SQS
- **Change Healthcare**: AWS SQS customer story highlights processing high-volume sensitive transaction workflows.
- **NASA Image Library**: AWS reference describes decoupling incoming jobs from processing pipelines.
- **Capital One**: AWS customer example highlights modernization of retail messaging/queueing architecture.
- **BMW**: AWS customer example references large-scale sensor/data ingestion workloads in cloud architectures.

Note:
- These are publicly referenced examples from vendor/community pages and case studies.
- Exact architecture details evolve over time; always validate current implementation details before making platform decisions.

## One-Line Rule of Thumb
- **SQS** = simplest managed queue.
- **Pulsar** = more powerful messaging + streaming platform.
