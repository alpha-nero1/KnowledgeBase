# 📦 Projects / BasicSQS
> *⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🤷‍♂️ What does it do?
A basic implementation of SQS. Leverages something called `LocalStack`. A really really super duper awesome tool that allows
you to emulate 60+ AWS services locally on your machine (including SQS!) and dev locally without mucking around setting up you AWS env, you can just learn how to use the services instead without worrying about the cloud for now!

## 🛠️ Project setup
sln: `dotnet new sln -n BasicSQS`
api: `dotnet new web -n api`
worker: `dotnet new worker -n worker`
setup: `dotnet sln add api/api.csproj worker/worker.csproj`
Spin up the localstack SQS: `docker compose up -d`
download aws cli: https://awscli.amazonaws.com/AWSCLIV2.msi
afterwards create your aws queue: 
```
aws --endpoint-url=http://localhost:4566 sqs create-queue --queue-name basic-queue
aws --endpoint-url=http://localhost:4566 sqs create-queue --queue-name basic-dlq
```
get the dlq artificial arn:
```
aws --endpoint-url=http://localhost:4566 sqs get-queue-attributes \
  --queue-url http://localhost:4566/000000000000/basic-dlq \
  --attribute-name QueueArn
```
attach the DLQ to the main one:
```
aws --endpoint-url=http://localhost:4566 sqs set-queue-attributes \
  --queue-url http://localhost:4566/000000000000/basic-queue \
  --attributes '{"RedrivePolicy":"{\"deadLetterTargetArn\":\"<ARN HERE>\",\"maxReceiveCount\":\"3\"}"}'
```

### What happens?
- If your worker receives a message and doesn't delete it, it becomes visible again.
- After 3 receives (as per maxReceiveCount), SQS automatically moves it to the DLQ.

## 🏎️ How to run 

## ⚖️ Final Remarks

## Usful Info
### 🔁 Life of a message (DLQ flow)
1. A message is sent to the main queue.
2. Your app receives it (via ReceiveMessage).
3. SQS hides it for VisibilityTimeout seconds (e.g. 30s).
4. Your app tries to process it.
5. If processing succeeds, you call DeleteMessage, and the message is gone forever ✅
6. If processing fails (exception, crash, timeout), and you do NOT call DeleteMessage, the message becomes visible again after the timeout ⏱️
7. This message is received again, and the ApproximateReceiveCount increases.
8. After maxReceiveCount (e.g. 3) is exceeded, SQS automatically moves the message to the DLQ

So you better be quick smart that your process takes less than the visibility timeout you set!



Useful emojis
👷🌐✅📦ℹ️⚡🧰