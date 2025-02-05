using BloodDonationSystem.Application.Services;
using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Exceptions;
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
            var validationResult = await _donorValidationService.ValidateCreateAddressForDonorAsync(request.DonorId);

            if (!validationResult.IsValid)
            {
                throw new ValidationException($"Erro: {string.Join("; ", validationResult.Errors)}");
            }

            var addressDto = await _cepService.GetAddressByCepAsync(request.Cep);

            var address = new Address(addressDto.Street, request.Number, addressDto.City, addressDto.State, addressDto.Cep, request.DonorId);

            await _addressRepository.CreateAsync(address);

            return address.Id;
        }
    }
}
