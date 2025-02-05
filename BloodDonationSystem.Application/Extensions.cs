using BloodDonationSystem.Application.Notifications;
using BloodDonationSystem.Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BloodDonationSystem.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddServices();
            services.AddNotifications();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {

            return services;
        }

        private static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddTransient<INotificationHandler<DonationCreatedEvent>, DonationCreatedNotificationHandler>();

            return services;
        }
    }
}
