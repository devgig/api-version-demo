using Demo.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Api
{
    public static class ServiceRegistrationExtension
    {
        public static void AddRegisteredServices(this IServiceCollection services)
        {
            services.AddTransient<IRentalService, RentalService>();
            services.AddTransient<IRentalValidator, RentalValidator>();

        }
    }
}
