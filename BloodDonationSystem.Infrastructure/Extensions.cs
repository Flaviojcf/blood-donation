using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Infrastructure.Persistance;
using BloodDonationSystem.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationSystem.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepository, AddressRepository>();


            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BloodDonationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

    }
}
