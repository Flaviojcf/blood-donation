using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateDonation
{
    public class CreateDonationCommand : IRequest<Guid>
    {
        public CreateDonationCommand(int quantityML, Guid donorId)
        {
            QuantityML = quantityML;
            DonorId = donorId;
        }

        public int QuantityML { get; set; }

        public Guid DonorId { get; set; }
    }
}
