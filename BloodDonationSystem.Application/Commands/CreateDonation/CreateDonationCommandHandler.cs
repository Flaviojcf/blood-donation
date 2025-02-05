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
        private readonly IMediator _mediator;

        public CreateDonationCommandHandler(IDonationRepository donationRepository, IDonorValidationService donorValidationService, IDonationValidationService donationValidationService, IMediator mediator)
        {
            _donationRepository = donationRepository;
            _donorValidationService = donorValidationService;
            _donationValidationService = donationValidationService;
            _mediator = mediator;
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

            try
            {
                foreach (var domainEvent in donation.GetDomainEvents().ToList())
                {
                    await _mediator.Publish(domainEvent, cancellationToken);
                }
                donation.ClearDomainEvents();
            }
            catch (Exception ex)
            {
                // Log do erro
                Console.WriteLine($"Erro ao publicar evento: {ex.Message}");
                throw;
            }


            donation.ClearDomainEvents();

            return donation.Id;
        }
    }
}
