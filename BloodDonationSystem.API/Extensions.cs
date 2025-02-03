using BloodDonationSystem.API.Filter;
using BloodDonationSystem.Application.Commands.CreateAddress;
using BloodDonationSystem.Application.Validators.Address;
using FluentValidation.AspNetCore;
using MediatR;

namespace BloodDonationSystem.API
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFilters();
            services.AddMediatR();
            return services;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateAddressCommand));

            return services;
        }

        private static IServiceCollection AddFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<TrimStringsActionFilter>();
                options.Filters.Add(typeof(ValidationFilter));
            }).AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateAddressCommandValidator>();
            });

            return services;
        }

    }
}
