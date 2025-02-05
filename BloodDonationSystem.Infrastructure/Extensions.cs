using BloodDonationSystem.Application.Services;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services;
using BloodDonationSystem.Domain.Services.Interfaces;
using BloodDonationSystem.Infrastructure.ExternalServices.SendGrid;
using BloodDonationSystem.Infrastructure.ExternalServices.ViaCep;
using BloodDonationSystem.Infrastructure.Persistance;
using BloodDonationSystem.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid.Extensions.DependencyInjection;

namespace BloodDonationSystem.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSqlServer(configuration);
            services.AddRepositories();
            services.AddServices();
            services.AddEmailService(configuration);
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IDonorRepository, DonorRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IDonationRepository, DonationRepository>();
            services.AddScoped<IBloodStockRepository, BloodStockRepository>();

            return services;
        }

        private static IServiceCollection AddSqlServer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BloodDonationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IDonorValidationService, DonorValidationService>();
            services.AddScoped<IDonationValidationService, DonationValidationService>();
            services.AddScoped<ICepService, ViaCepService>();
            return services;
        }

        private static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSendGrid(o =>
            {
                o.ApiKey = configuration.GetValue<string>("ApiKey");
            });

            services.AddScoped<IEmailService, SendGridService>();

            return services;
        }
    }
}
