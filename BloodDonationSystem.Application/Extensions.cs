using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationSystem.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {


            return services;
        }
    }
}
