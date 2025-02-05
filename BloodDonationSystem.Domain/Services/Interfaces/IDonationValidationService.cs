namespace BloodDonationSystem.Domain.Services.Interfaces
{
    public interface IDonationValidationService
    {
        bool ValidateDonationQuantity(int quantityML);
    }
}
