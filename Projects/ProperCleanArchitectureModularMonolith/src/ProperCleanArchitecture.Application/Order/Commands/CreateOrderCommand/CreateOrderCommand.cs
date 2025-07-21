using System.Text.Json.Serialization;
using MediatR;

namespace ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;

/// <summary>
/// Create/place an order.
/// </summary>
public record CreateOrderCommand(
    [property: JsonIgnore] int UserId,
    IEnumerable<Guid> Products
) : IRequest<Unit>;