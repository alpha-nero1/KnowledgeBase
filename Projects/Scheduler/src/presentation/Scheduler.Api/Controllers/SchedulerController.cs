using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Scheduling.Commands;

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
}