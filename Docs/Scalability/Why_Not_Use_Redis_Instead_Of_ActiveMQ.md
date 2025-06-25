## 🚦 Why not use Redis instead of ActiveMQ if it can "do" messaging
| Feature | **Redis** | **ActiveMQ** |
|--------|------------|-------------|
| **Purpose** | General-purpose data store + broker | Pure message broker |
| **Persistence** | Optional, best effort | Durable, transactional |
| **Protocol** | Custom commands / RESP | **AMQP**, MQTT, STOMP, JMS |
| **Latency** | Blazing fast (in-memory) | Slightly higher (disk-based) |
| **Pub/Sub reliability** | Volatile | Reliable delivery (ack, retry, DLQ) |
| **Scaling** | Easy to scale horizontally | Harder, but more guarantees |
| **Use cases** | Caching, simple queues, fast pub/sub | Guaranteed delivery, enterprise systems, complex routing |

---

## 📦 What is AMQP?
[AMQP](./AMQP.md)

---

## 🧠 Who Should Care?

You should care **if you need**:
- Mission-critical workflows
- Guaranteed delivery and processing
- Enterprise system integration (e.g., Java/JMS)
- Advanced message routing or auditing

Use **ActiveMQ**.

You can ignore it **if you need**:
- Fast, fire-and-forget messages
- Simplicity over ceremony
- Lightweight infrastructure

Use **Redis**.

---

## ⚡ TL;DR

> Redis is a fast in-memory Swiss Army knife that *can* do messaging.  
> ActiveMQ is a reliable, spec-compliant **messaging tank** built for high-stakes delivery.