using BloodDonationSystem.Domain.Records;

namespace BloodDonationSystem.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, IList<string>? roles = null);
        string GenerateRefreshToken(string email);
        string HashPassword(string password);
        Task<TokenValidationResultRecord> ValidateTokenAsync(string token);
    }
}