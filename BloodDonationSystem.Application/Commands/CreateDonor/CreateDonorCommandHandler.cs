using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;
using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateDonor
{
    public class CreateDonorCommandHandler : IRequestHandler<CreateDonorCommand, Guid>
    {
        private readonly IDonorRepository _donorRepository;

        public CreateDonorCommandHandler(IDonorRepository donorRepository, IDonorValidationService donorValidationService)
        {
            _donorRepository = donorRepository;
        }

        public async Task<Guid> Handle(CreateDonorCommand request, CancellationToken cancellationToken)
        {

            if (await _donorRepository.GetByEmail(request.Email) != null)
            {
                throw new ArgumentException("O endereço informado já foi cadastrado.");
            }

            var donor = new Donor(request.FullName, request.Email, request.BirthDate, request.GenderType, request.Weight, request.BloodType, request.RhFactorType);

            await _donorRepository.CreateAsync(donor);

            return donor.Id;
        }
    }
}
