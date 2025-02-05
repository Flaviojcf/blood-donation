using MediatR;

namespace BloodDonationSystem.Domain.Events
{
    public class DonationCreatedEvent : IDomainEvent, INotification
    {
        public Guid DonorId { get; }
        public int QuantityML { get; }

        public DonationCreatedEvent(Guid donationId, Guid donorId, int quantityML)
        {
            DonorId = donorId;
            QuantityML = quantityML;
        }
    }
}
