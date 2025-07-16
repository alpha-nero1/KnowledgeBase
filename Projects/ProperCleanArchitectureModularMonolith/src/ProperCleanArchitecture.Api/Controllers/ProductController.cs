using Microsoft.AspNetCore.Mvc;
using ProperCleanArchitecture.Application.Product.Queries;

namespace ProperCleanArchitecture.Api.Controllers;

public class ProductController : ApiControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ListAsync()
    {
        var products = await Mediator.Send(new ListProductsQuery());
        return Ok(products);
    }
}