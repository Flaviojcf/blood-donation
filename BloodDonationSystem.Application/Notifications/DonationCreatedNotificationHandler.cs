using BloodDonationSystem.Application.Services;
using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Events;
using BloodDonationSystem.Domain.Exceptions;
using BloodDonationSystem.Domain.Repositories;
using MediatR;

namespace BloodDonationSystem.Application.Notifications
{
    public class DonationCreatedNotificationHandler : INotificationHandler<DonationCreatedEvent>
    {
        private readonly IBloodStockRepository _bloodStockRepository;
        private readonly IDonorRepository _donorRepository;
        private readonly IEmailService _emailService;

        public DonationCreatedNotificationHandler(IBloodStockRepository bloodStockRepository, IDonorRepository donorRepository, IEmailService emailService)
        {
            _bloodStockRepository = bloodStockRepository;
            _donorRepository = donorRepository;
            _emailService = emailService;
        }

        public async Task Handle(DonationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var donor = await _donorRepository.GetByIdAsync(notification.DonorId);

            if (donor == null)
            {
                throw new NotFoundException($"Doador com o id '{notification.DonorId}' não foi encontrado.");
            }

            var bloodStock = await _bloodStockRepository.GetByBloodTypeAndRhFactorAsync(donor.BloodType, donor.RhFactorType);

            if (bloodStock == null)
            {
                BloodStock newBloodStock = new BloodStock(donor.BloodType, donor.RhFactorType, notification.QuantityML);
                await _bloodStockRepository.CreateAsync(newBloodStock);
            }

            if (bloodStock != null)
            {
                int newQuantity = bloodStock.QuantityML + notification.QuantityML;
                bloodStock.Update(newQuantity);
                await _bloodStockRepository.SaveChangesAsync();
            }

            if (bloodStock != null && bloodStock.QuantityML >= bloodStock.MinQuantityML)
            {
                await _emailService.SendAsync(donor.Email, "Meta atingida!!", $"Graças a sua doação, atingimos a quantidade mínima de {bloodStock.MinQuantityML}ML para o estoque, obrigado! ");
            }
        }
    }
}
