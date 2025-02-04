using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateAddress
{
    public class CreateAddressCommand : IRequest<Guid>
    {
        public CreateAddressCommand(int number, string cep, Guid donorId)
        {
            Number = number;
            Cep = cep;
            DonorId = donorId;
        }

        public int Number { get; set; }
        public string Cep { get; set; }
        public Guid DonorId { get; set; }
    }
}
