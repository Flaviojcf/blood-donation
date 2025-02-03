using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Guid>
    {
        private readonly IAddressRepository _addressRepository;
        public CreateAddressCommandHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var address = new Address(request.Street, request.Number, request.City, request.State, request.Cep);

            await _addressRepository.CreateAsync(address);

            return address.Id;
        }
    }
}
