using BloodDonationSystem.Domain.Constants;
using BloodDonationSystem.Domain.Enums;
using BloodDonationSystem.Domain.Exceptions;

namespace BloodDonationSystem.Domain.Entities
{
    public sealed class BloodStock : BaseEntity
    {
        public BloodStock(BloodType bloodType, RhFactorType rhFactorType, int quantityML)
        {
            ValidateCreateDomain(bloodType, rhFactorType, quantityML);
            BloodType = bloodType;
            RhFactorType = rhFactorType;
            QuantityML = quantityML;
        }

        public BloodType BloodType { get; private set; }
        public RhFactorType RhFactorType { get; private set; }
        public int QuantityML { get; private set; }

        public void Update(int quantityML)
        {
            ValidateUpdateDomain(quantityML);
            QuantityML = quantityML;
        }

        private static void ValidateCreateDomain(BloodType bloodType, RhFactorType rhFactor, int quantityML)
        {
            DomainException.When(!Enum.IsDefined(typeof(BloodType), bloodType), string.Format(DomainMessageConstants.messageFieldIsRequired, "bloodType"));
            DomainException.When(!Enum.IsDefined(typeof(RhFactorType), rhFactor), string.Format(DomainMessageConstants.messageFieldIsRequired, "rhFactor"));
            DomainException.When(quantityML <= 0, string.Format(DomainMessageConstants.messageFieldIsRequiredAndGreaterThan, "quantityML", 0));
        }

        private static void ValidateUpdateDomain(int quantityML)
        {
            DomainException.When(quantityML <= 0, string.Format(DomainMessageConstants.messageFieldIsRequiredAndGreaterThan, "quantityML", 0));
        }
    }
}
