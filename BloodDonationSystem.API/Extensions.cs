using BloodDonationSystem.API.Filters;
using BloodDonationSystem.Application.Commands.CreateAddress;
using BloodDonationSystem.Application.Validators.Address;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.OpenApi.Models;
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
            services.AddSwaggerAuthentication();
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

        private static IServiceCollection AddSwaggerAuthentication(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            return services;
        }

    }
}
