# 🛎️ Projects / Scheduler

## 🤷‍♂️ What does it do?

## 👷 How does it work?


## 🛠️ Project setup
### DB setup
This example works with PostgreSQL which runs reliably on all platforms including Apple Silicon Macs.
You can set it up by executing:

```bash
docker run --name dev-db \
  -e POSTGRES_PASSWORD=Password1 \
  -e POSTGRES_DB=scheduler \
  -p 5432:5432 \
  -v postgres-data:/var/lib/postgresql/data \
  -d postgres:15
```

### Database Migrations
From the API project directory:
```bash
cd src/presentation/Scheduler.Api
dotnet ef migrations add Initial --project ../../Scheduler.Infrastructure --startup-project .
dotnet ef database update --project ../../Scheduler.Infrastructure --startup-project .
```

## 🏎️ How to run 

## ⚖️ Final Remarks


Useful emojis
👷🌐✅📦ℹ️⚡🧰✔️

// Run from the API project that has a startup context.
dotnet ef migrations add Initial --project ../../Scheduler.Infrastructure --startup-project .
