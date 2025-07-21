namespace ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Unit>
{
    private readonly IMediator _mediator;

    public CreateOrderCommandHandler(
        IMediator mediator,
        IOrderRepository orderRepository
    )
    {
        _mediator = mediator;
    }

    public Task<Unit> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        // This is how we integrate with different features,
        // this mediator calls are what could later be replaced by API calls in
        // the event of microservisification.
        var products = await _mediator.Send(new ListProductsQuery());
        var productIds = new HashSet<int>(products.Select(product => product.ProductId));
        foreach (var product in command.Products)
        {
            if (productIds.Contains(productId))
            {
                throw new InvalidOrderCreationException("Cannot order non exsisting product");
            }
        }

        // Create and save order.

        // Return empty.
    }
}