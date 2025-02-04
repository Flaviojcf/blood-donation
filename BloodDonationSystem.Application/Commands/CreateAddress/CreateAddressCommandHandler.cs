using BloodDonationSystem.Application.Services;
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
        private readonly ICepService _cepService;
        public CreateAddressCommandHandler(IAddressRepository addressRepository, IDonorValidationService donorValidationService, ICepService cepService)
        {
            _addressRepository = addressRepository;
            _donorValidationService = donorValidationService;
            _cepService = cepService;
        }
        public async Task<Guid> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            if (!await _donorValidationService.IsDonorExistsAsync(request.DonorId))
            {
                throw new ArgumentException("O doador informado não existe.");
            }

            if (await _addressRepository.IsDonorAlreadyHasAddressAsync(request.DonorId))
            {
                throw new ArgumentException("O doador informado já possui endereço cadastrado.");
            }

            var addressDto = await _cepService.GetAddressByCepAsync(request.Cep);

            var address = new Address(addressDto.Street, request.Number, addressDto.City, addressDto.State, addressDto.Cep, request.DonorId);

            await _addressRepository.CreateAsync(address);

            return address.Id;
        }
    }
}
