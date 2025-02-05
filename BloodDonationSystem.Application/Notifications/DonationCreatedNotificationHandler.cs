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

        public DonationCreatedNotificationHandler(IBloodStockRepository bloodStockRepository, IDonorRepository donorRepository)
        {
            _bloodStockRepository = bloodStockRepository;
            _donorRepository = donorRepository;
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

            // Implementar notificação para o e-mail do adm
            //if (bloodStock.QuantityML < bloodStock.MinimumQuantity)
            //{
            //}
        }
    }
}
