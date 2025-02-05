using BloodDonationSystem.Domain.Events;
using MediatR;

namespace BloodDonationSystem.Application.Events
{
    public class DonationCreatedNotification : INotification
    {
        public Guid DonorId { get; }
        public int QuantityML { get; }

        public DonationCreatedNotification(DonationCreatedEvent domainEvent)
        {
            DonorId = domainEvent.DonorId;
            QuantityML = domainEvent.QuantityML;
        }
    }
}
