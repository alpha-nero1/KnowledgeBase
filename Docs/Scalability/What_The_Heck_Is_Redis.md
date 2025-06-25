# What the heck is Redis
RE = REmote
DI = DIctionary
S  = Service

> ℹ️ Redis is a server for key-value storage and is very nifty for things like **caching**, storing ephemeral data (when speed is far more
> important than durability) or even as a **message broker**

## 🔑 Core Concept: Key–Value Store

At its core, Redis is a dictionary:

```bash
SET user:123 "Alé"
GET user:123
```

But the "value" can be more than just a string — Redis supports:

- Strings
- Hashes (like JSON objects)
- Lists
- Sets
- Sorted Sets
- Streams
- Bitmaps, HyperLogLogs, and more

---

## 💬 Redis as a Message Broker

### 1. Pub/Sub (Publish/Subscribe)

This is like a real-time radio station.

- Publishers broadcast messages
- Subscribers receive messages **only if they're listening**

```bash
# Publisher
PUBLISH news "Hello world!"

# Subscriber
SUBSCRIBE news
```

> ⚠️ Messages are not stored. If no one is listening, they're lost.

### 2. Streams (Persistent Queues)

Redis Streams store messages durably with IDs. They support consumer groups, replay, and acknowledgment.

```bash
# Add message to stream
XADD mystream * user "Alé" message "Report ready"

# Read from stream
XREAD STREAMS mystream 0
```

> ✅ Durable  
> ✅ Replayable  
> ✅ Good for job queues, event logs, distributed processing

> ## ℹ️ How Can a Dictionary Be a Broker?
> Redis isn’t *just* a dictionary — it’s a **data structure server**. Some keys (like `mystream`) hold complex types, like message queues.
>
> So Redis is a dictionary where:
> - Some keys store user data
> - Some keys store logs (streams)
> - Some keys act as channels (for pub/sub)

## 🛠️ Common Use Cases

| Use Case | Redis Feature |
|----------|---------------|
| Caching results | Key–value store |
| User sessions | Hashes |
| Leaderboards | Sorted Sets |
| Chat / Notifications | Pub/Sub |
| Task queues | Streams |
| Rate limiting | Incr + Expiry |
| Real-time analytics | Bitmaps / HyperLogLogs |

## 🔥 A note on the speed
Redis Prioritizes Speed:
Redis keeps all data in memory (RAM). That’s why it's insanely fast — think microseconds.
> But RAM is volatile. If Redis crashes or your server restarts, you lose everything... unless you enable persistence.

You can enable persistence but **Redis doesn’t guarantee perfect durability like a traditional DB**

---

## ⚖️ Summary
Redis is:
- 🔥 Fast (everything in memory)
- 🧰 Flexible (many data types)
- ⚡ Great for real-time apps
- ✅ Capable of acting as a message broker using Pub/Sub or Streams
