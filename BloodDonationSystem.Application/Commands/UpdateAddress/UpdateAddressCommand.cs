using MediatR;

namespace BloodDonationSystem.Application.Commands.UpdateAddress
{
    public class UpdateAddressCommand : IRequest<Unit>
    {
        public UpdateAddressCommand(string street, int number, string city, string state, string cep)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            Cep = cep;
        }

        public Guid Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cep { get; set; }
    }
}
