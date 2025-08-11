using MediatR;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Application.Scheduling.Commands;

namespace Scheduler.Api.Controllers;

/// <summary>
/// Controller for managing CRON job scheduling.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CronJobsController(IMediator _mediator) : ControllerBase
{
    /// <summary>
    /// Schedule a recurring job with a custom CRON expression.
    /// </summary>
    /// <param name="request">The CRON job scheduling request</param>
    /// <returns>The job ID of the scheduled recurring job</returns>
    [HttpPost]
    public async Task<ActionResult<string>> ScheduleCronJob([FromBody] ScheduleCronJobRequest request)
    {
        var command = new ScheduleCronJobCommand(request.JobId, request.CronExpression, request.OrderId);
        var jobId = await _mediator.Send(command);
        return Ok(jobId);
    }

    /// <summary>
    /// Schedule a daily job that runs at midnight UTC.
    /// </summary>
    /// <param name="request">The daily job scheduling request</param>
    /// <returns>The job ID of the scheduled daily job</returns>
    [HttpPost("daily-midnight")]
    public async Task<ActionResult<string>> ScheduleDailyMidnightJob([FromBody] ScheduleDailyMidnightJobRequest request)
    {
        var command = new ScheduleDailyMidnightJobCommand(request.JobId, request.OrderId);
        var jobId = await _mediator.Send(command);
        return Ok(jobId);
    }

    /// <summary>
    /// Remove a recurring CRON job.
    /// </summary>
    /// <param name="jobId">The ID of the job to remove</param>
    /// <returns>Success status</returns>
    [HttpDelete("{jobId}")]
    public async Task<ActionResult> RemoveCronJob(string jobId)
    {
        var command = new RemoveCronJobCommand(jobId);
        await _mediator.Send(command);
        return NoContent();
    }
}

/// <summary>
/// Request model for scheduling CRON jobs.
/// </summary>
public class ScheduleCronJobRequest
{
    public required string JobId { get; set; }
    public required string CronExpression { get; set; }
    public int OrderId { get; set; }
}

/// <summary>
/// Request model for scheduling daily midnight jobs.
/// </summary>
public class ScheduleDailyMidnightJobRequest
{
    public required string JobId { get; set; }
    public int OrderId { get; set; }
}
