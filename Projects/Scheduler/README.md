# 🛎️ Projects / Scheduler + CRON
CRON = chronos (time in greco)

## 🤷‍♂️ What does it do?
This is a fantastic scheduling project that demonstrates how to leverage hangfire to acheive some really awesome stuff.
So what has this project acheived:
1. Hangfire using postgres as a DB
2. Api & Worker hangfire split up (API fires jobs, Worker picks them up and processes them)
3. Scheduling a job to occur in the future! (using a super generic API)
5. Scheduling a recurring job based of the schedule you provide at API request (using a super generic API)
6. Tracking the data in our own DB for reference and tracking.

## 👷 How does it work?
The project is built using Clean Architecture principles with a clear separation of concerns:

### Architecture Overview
- **API Layer** (`Scheduler.Api`): RESTful API that receives scheduling requests and exposes Swagger documentation + Hangfire dashboard
- **Worker Layer** (`Scheduler.Worker`): Background service that processes scheduled jobs
- **Application Layer** (`Scheduler.Application`): Business logic, command/query handlers using MediatR pattern
- **Infrastructure Layer** (`Scheduler.Infrastructure`): Data persistence, Hangfire configuration, and external services
- **Domain Layer** (`Scheduler.Domain`): Core entities and business rules
- **Contracts Layer** (`Scheduler.Contracts`): Shared DTOs and interfaces

### Job Execution Flow
1. **Job Scheduling**: Client sends a request to the API with job details (OrderId, Type, ExecuteAt/CronExpression)
2. **Database Storage**: Job details are stored in PostgreSQL with status tracking
3. **Hangfire Scheduling**: Job is queued in Hangfire with the specified execution time
4. **Worker Processing**: The Worker service picks up the job when it's time to execute
5. **Job Execution**: The appropriate `IFutureJobExecutor` implementation handles the actual work
6. **Status Updates**: Job status is updated throughout the lifecycle (Pending → Scheduled → Processing → Completed/Failed)

### Job Types
The system supports different job types via the `FutureJobType` enum:
- **SimpleLog**: Basic logging job for demonstration
- **MidnightCleanupJob**: Recurring cleanup tasks

### Key Components
- **SchedulerService**: Main service for job scheduling, cancellation, and rescheduling
- **IFutureJobExecutor**: Interface for implementing custom job logic
- **FutureJob Entity**: Domain model that tracks job state and execution details
- **Command/Query Handlers**: MediatR handlers for processing API requests

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

If you need to clean up the persistent volume: `docker volume rm postgres-data`

### Database Migrations
From the API project directory:
```bash
cd src/presentation/Scheduler.Api
dotnet ef migrations add Initial --project ../../Scheduler.Infrastructure --startup-project .
dotnet ef database update --project ../../Scheduler.Infrastructure --startup-project .
```

## 🏎️ How to run 
1. Run Api project and open `http://localhost:5000/swagger` to see the endpoints.
2. Run Worker project
3. If you visit `http://localhost:5000/hangfire` you will see the hangfire dashboard.

## ⚖️ Final Remarks
This project demonstrates how to use hangfire and how to solve for problems of doing work in the future or recurring, this is a great reference to have when facing these challenges in the future.
Up the hangfire!

