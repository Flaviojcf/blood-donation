using BloodDonationSystem.Domain.Constants;
using BloodDonationSystem.Domain.Exceptions;

namespace BloodDonationSystem.Domain.Entities
{
    public sealed class Donation : BaseEntity
    {
        public Donation(DateTime donationDate, int quantityML, Guid donorId)
        {
            ValidateDomain(donationDate, quantityML, donorId);
            DonationDate = donationDate;
            QuantityML = quantityML;
            DonorId = donorId;
        }

        public DateTime DonationDate { get; private set; }
        public int QuantityML { get; private set; }

        public Guid DonorId { get; private set; }
        public Donor? Donor { get; private set; }

        public void Update(DateTime donationDate, int quantityML, Guid donorId)
        {
            ValidateDomain(donationDate, quantityML, donorId);
            DonationDate = donationDate;
            QuantityML = quantityML;
            DonorId = donorId;
        }

        private static void ValidateDomain(DateTime donationDate, int quantityML, Guid donorId)
        {
            DomainException.When(donationDate == DateTime.MinValue, string.Format(DomainMessageConstants.messageFieldIsRequired, "donationDate"));
            DomainException.When(quantityML <= 0, string.Format(DomainMessageConstants.messageFieldIsRequiredAndGreaterThan, "quantityML", 0));
            DomainException.When(donorId == Guid.Empty, string.Format(DomainMessageConstants.messageFieldIsRequired, "donorId"));
        }
    }
}
