using BloodDonationSystem.Domain.Services.Interfaces;

namespace BloodDonationSystem.Domain.Services
{
    public class DonationValidationService : IDonationValidationService
    {

        public bool ValidateDonationQuantity(int quantityML)
        {
            if (quantityML > 470 || quantityML < 420) return false;

            return true;
        }
    }
}
