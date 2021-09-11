using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesTaxes.App.Apps;
using SalesTaxes.Domain.Apps;
using SalesTaxes.Domain.Notifications;

namespace SalesTaxes.Infra.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //App
            services.AddScoped<ISalesApp, SalesApp>();

            //Domain
            services.AddScoped<INotifier, Notifier>();
        }
    }
}