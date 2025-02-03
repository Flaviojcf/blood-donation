using BloodDonationSystem.Domain.Repositories;
using MediatR;

namespace BloodDonationSystem.Application.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, Unit>
    {
        private readonly IAddressRepository _addressRepository;
        public DeleteAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<Unit> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetByIdAsync(request.Id);

            address?.Deactivate();

            await _addressRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
