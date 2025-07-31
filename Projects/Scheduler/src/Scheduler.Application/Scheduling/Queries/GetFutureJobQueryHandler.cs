using MediatR;
using Scheduler.Application.Scheduling.Interfaces;
using Scheduler.Contracts.Scheduling;
using Scheduler.Domain.Entities;

namespace Scheduler.Application.Scheduling.Queries;

public class GetFutureJobQueryHandler : IRequestHandler<GetFutureJobQuery, FutureJobDto>
{
    private readonly IFutureJobRepository _futureJobRepository;

    public GetFutureJobQueryHandler(IFutureJobRepository futureJobRepository)
    {
        _futureJobRepository = futureJobRepository;
    }

    public async Task<FutureJobDto> Handle(GetFutureJobQuery request, CancellationToken cancellationToken)
    {
        var job = await _futureJobRepository.GetByIdAsync(request.FutureJobId);
        if (job == null) {
            throw new KeyNotFoundException($"Future job with FutureJobId {request.FutureJobId} not found.");
        }
        return new FutureJobDto
        {
            FutureJobId = job.FutureJobId,
            OrderId = job.OrderId,
            Type = job.Type,
            ExecuteAt = job.ExecuteAt,
            CreatedAt = job.CreatedAt,
            Status = job.Status
        };
    }
}
