using MediatR;

namespace BloodDonationSystem.Application.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<Unit>
    {
        public DeleteAddressCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
