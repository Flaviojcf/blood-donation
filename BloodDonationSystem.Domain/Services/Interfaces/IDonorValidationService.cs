namespace BloodDonationSystem.Domain.Services.Interfaces
{
    public interface IDonationValidationService
    {
        bool IsLegalForDonation(int quantityML);
    }
}
