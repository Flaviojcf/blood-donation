namespace BloodDonationSystem.Domain.Services
{
    public interface IDonorValidationService
    {
        Task<bool> DonorExistsAsync(Guid donorId);
    }
}
