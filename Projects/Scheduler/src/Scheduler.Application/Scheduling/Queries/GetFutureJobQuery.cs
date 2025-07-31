using MediatR;
using Scheduler.Contracts.Scheduling;

namespace Scheduler.Application.Scheduling.Queries;

/// <summary>
/// Query to get a future job by its ID.
/// </summary>
public record GetFutureJobQuery(int FutureJobId) : IRequest<FutureJobDto>;
