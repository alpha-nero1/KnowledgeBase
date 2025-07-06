using api.services;
using Microsoft.AspNetCore.Mvc;

namespace api.controllers;

public record PublishRequest(string Value);

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{
    private readonly IPublisherService _publisher;
    private readonly ILogger<MessageController> _log;

    public MessageController(
        IPublisherService publisher,
        ILogger<MessageController> log
    )
    {
        _publisher = publisher;
        _log = log;
    }

    [HttpPost]
    public async Task<IActionResult> Publish(
        [FromBody] PublishRequest req,
        CancellationToken ct
    )
    {
        if (string.IsNullOrWhiteSpace(req.Value)) return BadRequest("Value cannot be empty");

        await _publisher.PublishAsync(req.Value, ct);
        _log.LogInformation("Sent: {Value}", req.Value);
        return Accepted(new { req.Value });
    }
}
