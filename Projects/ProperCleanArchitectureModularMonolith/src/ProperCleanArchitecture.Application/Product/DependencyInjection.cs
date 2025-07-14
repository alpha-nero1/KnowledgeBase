using Microsoft.Extensions.DependencyInjection;

namespace ProperCleanArchitecture.Application.Product;

public static class ProductDependencyInjection
{
    public static IServiceCollection AddProduct(this IServiceCollection services)
    {
        return services;
    }
}