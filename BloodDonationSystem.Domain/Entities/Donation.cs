using BloodDonationSystem.Domain.Constants;
using BloodDonationSystem.Domain.Events;
using BloodDonationSystem.Domain.Exceptions;

namespace BloodDonationSystem.Domain.Entities
{
    public sealed class Donation : BaseEntity
    {
        public Donation(int quantityML, Guid donorId)
        {
            ValidateDomain(quantityML, donorId);
            DonationDate = DateTime.Now;
            QuantityML = quantityML;
            DonorId = donorId;

            AddEvent(new DonationCreatedEvent(Id, DonorId, QuantityML));
        }

        public DateTime DonationDate { get; private set; }
        public int QuantityML { get; private set; }

        public Guid DonorId { get; private set; }
        public Donor? Donor { get; private set; }

        public List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

        public void Update(int quantityML, Guid donorId)
        {
            ValidateDomain(quantityML, donorId);
            DonationDate = DateTime.Now;
            QuantityML = quantityML;
            DonorId = donorId;
        }

        private static void ValidateDomain(int quantityML, Guid donorId)
        {
            DomainException.When(quantityML <= 0, string.Format(DomainMessageConstants.messageFieldIsRequiredAndGreaterThan, "quantityML", 0));
            DomainException.When(donorId == Guid.Empty, string.Format(DomainMessageConstants.messageFieldIsRequired, "donorId"));
        }

        private void AddEvent(IDomainEvent @event)
        {
            if (_domainEvents == null)
                _domainEvents = new List<IDomainEvent>();

            _domainEvents.Add(@event);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
