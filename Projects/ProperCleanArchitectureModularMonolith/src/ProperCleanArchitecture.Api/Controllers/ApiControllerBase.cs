using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProperCleanArchitecture.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// ISender, smaller interface from mediator focused on only sending.
    /// </summary>
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}