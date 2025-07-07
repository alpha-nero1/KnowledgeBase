using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using common;
using common.options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace api.controllers;

[ApiController]
[Route("[controller]")]
public class SqsController : ControllerBase
{
    private readonly IAmazonSQS _sqs;
    private readonly string _queueUrl;
    public SqsController(IAmazonSQS sqs, IOptions<AwsOptions> options)
    {
        _sqs = sqs;
        _queueUrl = options.Value.SQS.QueueUrl;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] BasicMessage message)
    {
        var request = new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = JsonSerializer.Serialize(message)
        };

        var response = await _sqs.SendMessageAsync(request);
        return Ok(response.MessageId);
    }
}