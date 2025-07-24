using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;

namespace LocalStackS3.Controllers;

[ApiController]
[Route("[controller]")]
public class ContentController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName = "localstack-bucket";

    public ContentController(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        using var stream = file.OpenReadStream();
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = file.FileName,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(putRequest);
        return Ok(new { file = file.FileName });
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetFile([FromRoute] string key)
    {
        try
        {
            var response = await _s3Client.GetObjectAsync(_bucketName, key);
            return File(response.ResponseStream, response.Headers.ContentType ?? "application/octet-stream", key);
        }
        catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }
    }
}
