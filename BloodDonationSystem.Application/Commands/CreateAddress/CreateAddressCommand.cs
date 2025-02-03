using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateAddress
{
    public class CreateAddressCommand : IRequest<Guid>
    {
        public CreateAddressCommand(string street, int number, string city, string state, string cep, Guid donorId)
        {
            Street = street;
            Number = number;
            City = city;
            State = state;
            Cep = cep;
            DonorId = donorId;
        }

        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cep { get; set; }
        public Guid DonorId { get; set; }
    }
}
