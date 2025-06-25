# 📦 What is AMQP?

**AMQP** stands for **Advanced Message Queuing Protocol**. It’s like the HTTP of messaging — a standardized way for systems to communicate using messages reliably and predictably.

## AMQP brings:
- 🛡️ **Guaranteed delivery**
- 📬 **Message acknowledgments**
- 🔁 **Retries & dead-letter queues**
- 🔀 **Flexible routing & filtering**
- 📜 **Transactional messaging**

AMQP is what gives message brokers like ActiveMQ, RabbitMQ, and Azure Service Bus their enterprise-grade reliability.

> ℹ️ Redis does **not** use AMQP — it trades strict guarantees for speed and simplicity.

## What uses AMQP and what does not?
### What uses AMQP
| Technology                              | Role                  | Notes                                 |
| --------------------------------------- | --------------------- | ------------------------------------- |
| **RabbitMQ**                            | ✅ Full AMQP support   | Most popular AMQP broker; easy to use |
| **Apache ActiveMQ (Classic & Artemis)** | ✅ AMQP support        | Artemis is more modern and performant |
| **Azure Service Bus**                   | ✅ AMQP 1.0            | Microsoft's cloud messaging system    |
| **Qpid** (by Apache)                    | ✅ AMQP implementation | Heavy enterprise/Java use             |
| **Red Hat AMQ**                         | ✅ Based on Artemis    | Enterprise-focused with AMQP support  |
| **Amazon MQ**                           | ✅ Supports AMQP       | Managed ActiveMQ/RabbitMQ on AWS      |
| **CloudAMQP**                           | ✅ Hosted RabbitMQ     | Cloud provider, uses AMQP underneath  |


### What does not
- **SQS**: does not use AMQP — it uses Amazon’s own proprietary protocol over HTTP/S
- **Redis**: Custom protocol (RESP)
- **Kafka**: Custom TCP-based protocol
- **MQTT**: Lightweight IoT messaging protocol (not AMQP)

## 🏆 Why People Often Prefer RabbitMQ
### ✅ 1. Better Developer Experience
- Easier to get started
- Better tooling, better docs
- Clear concepts (exchanges, queues, bindings)

### ✅ 2. Pluggable and Flexible
- Delayed message plugin
- Dead-lettering, priorities, TTL
- Supports multiple protocols via plugins

### ✅ 3. Rich Ecosystem
- DevOps tools
- Monitoring tools (Prometheus exporters, etc.)
- Works well across many languages

