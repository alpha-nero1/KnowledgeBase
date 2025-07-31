using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Scheduling.Commands;
using Scheduler.Application.Scheduling.Queries;

namespace Scheduler.Api.Controllers;

public class SchedulerController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public SchedulerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ScheduleJobAsync([FromBody] ScheduleJobCommand request)
    {
        var jobId = await _mediator.Send(request);
        return Ok(new { JobId = jobId });
    }

    [HttpGet("{futureJobId:int}")]
    public async Task<IActionResult> GetFutureJobAsync(int futureJobId)
    {
        var jobId = await _mediator.Send(new GetFutureJobQuery(futureJobId));
        return Ok(new { JobId = jobId });
    }
}