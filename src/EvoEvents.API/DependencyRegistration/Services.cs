using EvoEvents.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EvoEvents.API.DependencyRegistration
{
    public static class Services
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<EvoEventsContext, EvoEventsContext>();
        }
    }
}
