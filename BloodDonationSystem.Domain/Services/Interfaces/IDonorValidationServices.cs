using BloodDonationSystem.Domain.Validations;

namespace BloodDonationSystem.Domain.Services.Interfaces
{
    public interface IDonorValidationService
    {
        Task<ValidationResult> ValidateCreateAddressForDonorAsync(Guid donorId);
        Task<ValidationResult> ValidateCreateDonorAsync(string email);
        Task<ValidationResult> ValidateDonorForDonationAsync(Guid donorId);
    }
}
