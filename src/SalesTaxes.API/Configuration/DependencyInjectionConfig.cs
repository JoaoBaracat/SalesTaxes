using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesTaxes.Infra.IoC;
using System;

namespace SalesTaxes.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services, configuration);
        }
    }
}