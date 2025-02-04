using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;
using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, Guid>
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IDonorValidationService _donorValidationService;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IDonorValidationService donorValidationService)
        {
            _addressRepository = addressRepository;
            _donorValidationService = donorValidationService;
        }
        public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if (!await _donorValidationService.IsDonorExistsAsync(request.DonorId))
            {
                throw new ArgumentException("O doador informado não existe.");
            }

            var address = new Address(request.Street, request.Number, request.City, request.State, request.Cep, request.DonorId);

            await _addressRepository.CreateAsync(address);

            return address.Id;
        }
    }
}
