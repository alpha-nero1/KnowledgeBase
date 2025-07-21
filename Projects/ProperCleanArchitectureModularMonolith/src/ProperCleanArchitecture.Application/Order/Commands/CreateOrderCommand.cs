namespace ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;

public record CreateOrderCommand(
    int UserId,
    IEnumerable<int> Products,
) : IRequest<Unit>;