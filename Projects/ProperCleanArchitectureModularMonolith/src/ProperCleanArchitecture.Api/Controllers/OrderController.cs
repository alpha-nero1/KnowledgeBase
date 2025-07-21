using Microsoft.AspNetCore.Mvc;
using ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;

namespace ProperCleanArchitecture.Api.Controllers;

public class OrderController : ApiControllerBase
{
    [HttpPost("/api/orders")]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
    {
        // STUB: Replace user ID with that from Auth.
        return Ok(await Mediator.Send(new CreateOrderCommand(1, request.Products)));
    }
}