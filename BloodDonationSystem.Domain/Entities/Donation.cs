using BloodDonationSystem.Domain.Constants;
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
        }

        public DateTime DonationDate { get; private set; }
        public int QuantityML { get; private set; }

        public Guid DonorId { get; private set; }
        public Donor? Donor { get; private set; }

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
    }
}
