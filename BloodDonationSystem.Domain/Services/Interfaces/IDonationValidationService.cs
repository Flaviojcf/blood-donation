namespace BloodDonationSystem.Domain.Services.Interfaces
{
    public interface IDonorValidationService
    {
        Task<bool> IsDonorExistsAsync(Guid donorId);
        Task<bool> IsDonorEmailAlreadyExistsAsync(string donorEmail);
        Task<bool> IsLegalForDonation(Guid donorId);
    }
}
