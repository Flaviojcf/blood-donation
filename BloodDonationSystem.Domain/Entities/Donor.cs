using BloodDonationSystem.Domain.Constants;
using BloodDonationSystem.Domain.Enums;
using BloodDonationSystem.Domain.Exceptions;

namespace BloodDonationSystem.Domain.Entities
{
    public sealed class Donor : BaseEntity
    {
        public Donor(string fullName, string email, string password, DateTime birthDate, GenderType genderType, decimal weight, BloodType bloodType, RhFactorType rhFactorType)
        {
            ValidateDomain(fullName, email, password, birthDate, genderType, weight, bloodType, rhFactorType);
            FullName = fullName;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            GenderType = genderType;
            Weight = weight;
            BloodType = bloodType;
            RhFactorType = rhFactorType;
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime BirthDate { get; private set; }
        public GenderType GenderType { get; private set; }
        public decimal Weight { get; private set; }
        public BloodType BloodType { get; private set; }
        public RhFactorType RhFactorType { get; private set; }

        public Address? Address { get; private set; }
        public IEnumerable<Donation>? Donations { get; private set; }


        public void Update(string fullName, string email, string password, DateTime birthDate, GenderType genderType, decimal weight, BloodType bloodType, RhFactorType rhFactorType)
        {
            ValidateDomain(fullName, email, password, birthDate, genderType, weight, bloodType, rhFactorType);
            FullName = fullName;
            Email = email;
            Password = password;
            BirthDate = birthDate;
            GenderType = genderType;
            Weight = weight;
            BloodType = bloodType;
            RhFactorType = rhFactorType;
        }

        private static void ValidateDomain(string fullName, string email, string password, DateTime? birthDate, GenderType genderType, decimal weight, BloodType bloodType, RhFactorType rhFactorType)
        {
            DomainException.When(string.IsNullOrEmpty(fullName), string.Format(DomainMessageConstants.messageFieldIsRequired, "fullName"));
            DomainException.When(string.IsNullOrEmpty(email), string.Format(DomainMessageConstants.messageFieldIsRequired, "email"));
            DomainException.When(string.IsNullOrEmpty(password), string.Format(DomainMessageConstants.messageFieldIsRequired, "password"));
            DomainException.When(birthDate == DateTime.MinValue, string.Format(DomainMessageConstants.messageFieldIsRequired, "birthDate"));
            DomainException.When(!Enum.IsDefined(typeof(GenderType), genderType), string.Format(DomainMessageConstants.messageFieldIsRequired, "gender"));
            DomainException.When(weight <= 0, string.Format(DomainMessageConstants.messageFieldIsRequiredAndGreaterThan, "weight", 0));
            DomainException.When(!Enum.IsDefined(typeof(BloodType), bloodType), string.Format(DomainMessageConstants.messageFieldIsRequired, "bloodType"));
            DomainException.When(!Enum.IsDefined(typeof(RhFactorType), rhFactorType), string.Format(DomainMessageConstants.messageFieldIsRequired, "rhFactor"));
        }

    }
}
