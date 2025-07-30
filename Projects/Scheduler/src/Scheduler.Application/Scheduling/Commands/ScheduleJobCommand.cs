using MediatR;
using Scheduler.Contracts.Scheduling;

namespace Scheduler.Application.Scheduling.Commands;

public record ScheduleJobCommand(Job Job) : IRequest<string>;
