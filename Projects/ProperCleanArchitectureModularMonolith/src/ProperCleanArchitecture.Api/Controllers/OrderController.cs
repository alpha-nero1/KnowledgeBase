using Microsoft.AspNetCore.Mvc;

namespace ProperCleanArchitecture.Api.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        return Ok();
    }
}