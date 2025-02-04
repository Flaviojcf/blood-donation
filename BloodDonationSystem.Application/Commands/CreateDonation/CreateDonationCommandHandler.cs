using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;
using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateDonation
{
    public class CreateDonationCommandHandler : IRequestHandler<CreateDonationCommand, Guid>
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IDonorValidationService _donorValidationService;
        private readonly IDonationValidationService _donationValidationService;

        public CreateDonationCommandHandler(IDonationRepository donationRepository, IDonorValidationService donorValidationService, IDonationValidationService donationValidationService)
        {
            _donationRepository = donationRepository;
            _donorValidationService = donorValidationService;
            _donationValidationService = donationValidationService;
        }

        public async Task<Guid> Handle(CreateDonationCommand request, CancellationToken cancellationToken)
        {

            if (!await _donorValidationService.IsDonorExistsAsync(request.DonorId))
            {
                throw new ArgumentException("O doador informado não existe.");
            }

            if (!await _donorValidationService.IsLegalForDonation(request.DonorId))
            {
                throw new ArgumentException("O doador informado não está aptado para doar.");
            }

            if (!_donationValidationService.IsLegalForDonation(request.QuantityML))
            {
                throw new ArgumentException("O doador informado não está aptado para doar.");
            }

            var donation = new Donation(request.QuantityML, request.DonorId);

            await _donationRepository.CreateAsync(donation);

            return donation.Id;
        }
    }
}
