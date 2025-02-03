using BloodDonationSystem.Domain.Constants;
using BloodDonationSystem.Domain.Exceptions;

namespace BloodDonationSystem.Domain.Entities
{
    public sealed class Address
    {
        public Address(string street, int number, string city, string state, string cep)
        {
            ValidateDomain(street, number, city, state, cep);
            Street = street;
            Number = number;
            City = city;
            State = state;
            Cep = cep;
        }

        public string Street { get; private set; }
        public int Number { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Cep { get; private set; }

        public void Update(string street, int number, string city, string state, string cep)
        {
            ValidateDomain(street, number, city, state, cep);
            Street = street;
            Number = number;
            City = city;
            State = state;
            Cep = cep;
        }

        private static void ValidateDomain(string street, int number, string city, string state, string cep)
        {
            DomainException.When(string.IsNullOrEmpty(street), string.Format(DomainMessageConstants.messageFieldIsRequired, "street"));
            DomainException.When(number == 0, string.Format(DomainMessageConstants.messageFieldIsRequiredAndGreaterThan, "number", 0));
            DomainException.When(string.IsNullOrEmpty(city), string.Format(DomainMessageConstants.messageFieldIsRequired, "city"));
            DomainException.When(string.IsNullOrEmpty(state), string.Format(DomainMessageConstants.messageFieldIsRequired, "state"));
            DomainException.When(string.IsNullOrEmpty(cep), string.Format(DomainMessageConstants.messageFieldIsRequired, "cep"));
        }
    }
}
