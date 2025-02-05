﻿using BloodDonationSystem.API.Filters;
using BloodDonationSystem.Application.Commands.CreateAddress;
using BloodDonationSystem.Application.Validators.Address;
using FluentValidation.AspNetCore;
using MediatR;
using System.Text.Json.Serialization;

namespace BloodDonationSystem.API
{
    public static class Extensions
    {
        public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFilters();
            services.AddMediatR();
            services.IgnoreCycle();
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
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<CreateAddressCommandValidator>();
            });

            return services;
        }

        private static IServiceCollection IgnoreCycle(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            return services;
        }

    }
}
