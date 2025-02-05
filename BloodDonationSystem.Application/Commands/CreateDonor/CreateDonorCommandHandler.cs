using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Exceptions;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;
using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateDonor
{
    public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, Guid>
    {
        private readonly IDonorRepository _donorRepository;
        private readonly IDonorValidationService _donorValidationService;

        public CreateDonorCommandHandler(IDonorRepository donorRepository, IDonorValidationService donorValidationService)
        {
            _donorRepository = donorRepository;
            _donorValidationService = donorValidationService;
        }

        public async Task<Guid> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _donorValidationService.ValidateCreateDonorAsync(request.Email);

            if (!validationResult.IsValid)
            {
                throw new ValidationException($"{string.Join("; ", validationResult.Errors)}");
            }

            var donor = new Donor(request.FullName, request.Email, request.BirthDate, request.GenderType, request.Weight, request.BloodType, request.RhFactorType);

            await _donorRepository.CreateAsync(donor);

            return donor.Id;
        }
    }
}
