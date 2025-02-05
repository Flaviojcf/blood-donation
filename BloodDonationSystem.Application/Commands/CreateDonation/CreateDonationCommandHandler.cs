using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Exceptions;
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
            var validationResult = await _donorValidationService.ValidateDonorForDonationAsync(request.DonorId);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.ToList();
                throw new ValidationException(errors);
            }


            if (!_donationValidationService.ValidateDonationQuantity(request.QuantityML))
            {
                throw new ValidationException("A quantidade para doação deve estar entre 420ml e 470ml.");
            }

            var donation = new Donation(request.QuantityML, request.DonorId);

            await _donationRepository.CreateAsync(donation);

            return donation.Id;
        }
    }
}
