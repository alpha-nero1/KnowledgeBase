using MediatR;
using ProperCleanArchitecture.Application.Order.Exceptions;
using ProperCleanArchitecture.Application.Order.Interfaces;
using ProperCleanArchitecture.Application.Product.Queries.ListProducts;

namespace ProperCleanArchitecture.Application.Order.Commands.CreateOrderCommand;

public class CreateOrderCommandHandler(
    ISender mediator,
    IOrderRepository orderRepository
) : IRequestHandler<CreateOrderCommand, Unit>
{
    private readonly ISender _mediator = mediator;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Unit> Handle(CreateOrderCommand command, CancellationToken ct)
    {
        // This is how we integrate with different features,
        // this mediator calls are what could later be replaced by API calls in
        // the event of microservisification.
        var products = await _mediator.Send(new ListProductsQuery(), ct);
        var productIds = new HashSet<Guid>(products.Select(product => product.ProductId));
        foreach (var productId in command.Products)
        {
            if (productIds.Contains(productId))
            {
                throw new InvalidOrderCreationException(
                    "Cannot order non exsisting product"
                    , new Dictionary<string, object> { { "ProductId", productId } 
                });
            }
        }

        await CreateOrdersAsync(command.UserId, command.Products, ct);

        // Done...
        return Unit.Value;
    }

    private async Task CreateOrdersAsync(int userId, IEnumerable<Guid> productIds, CancellationToken ct)
    {
        // STUB: Replace with actual parent GUID.
        var parentProductId = Guid.NewGuid();
        var parent = await _orderRepository.CreateAsync(new Domain.Entities.Order
        {
            UserId = userId,
            ProductId = parentProductId
        }, ct);

        await _orderRepository.CreateBulkAsync(
            productIds.Select(productId => new Domain.Entities.Order
            {
                UserId = userId,
                ProductId = productId,
                ParentOrderId = parent.OrderId
            }), ct);
    }
}