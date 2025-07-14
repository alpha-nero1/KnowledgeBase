using Microsoft.Extensions.DependencyInjection;
using ProperCleanArchitecture.Application.Product;
using ProperCleanArchitecture.Application.Product.Profiles;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region Core Setup

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationDependencyInjection).Assembly));
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(ProductProfile).Assembly);
        });

        #endregion

        #region Feature level injection

        ProductDependencyInjection.AddProduct(services);
        SharedDependencyInjection.AddShared(services);

        #endregion
        return services;
    }
}
