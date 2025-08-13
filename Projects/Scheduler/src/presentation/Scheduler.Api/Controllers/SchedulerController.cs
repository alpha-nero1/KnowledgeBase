using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Scheduling.Commands;
using Scheduler.Application.Scheduling.Commands.ScheduleCronJob;
using Scheduler.Application.Scheduling.Commands.ScheduleFutureJob;
using Scheduler.Application.Scheduling.Queries;

namespace Scheduler.Api.Controllers;

public class SchedulerController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ScheduleJobAsync([FromBody] ScheduleFutureJobCommand request)
    {
        var jobId = await Mediator.Send(request);
        return Ok(new { JobId = jobId });
    }

    [HttpPost("cron")]
    public async Task<IActionResult> ScheduleCronJobAsync([FromBody] ScheduleCronJobCommand request)
    {
        var jobId = await Mediator.Send(request);
        return Ok(new { JobId = jobId });
    }

    [HttpGet("{futureJobId:int}")]
    public async Task<IActionResult> GetFutureJobAsync(int futureJobId)
    {
        var jobId = await Mediator.Send(new GetFutureJobQuery(futureJobId));
        return Ok(new { JobId = jobId });
    }

    [HttpDelete("{futureJobId:int}")]
    public async Task<IActionResult> CancelFutureJob(int futureJobId)
    {
        await Mediator.Send(new CancelScheduledJobCommand(futureJobId));
        return NoContent();
    }
}