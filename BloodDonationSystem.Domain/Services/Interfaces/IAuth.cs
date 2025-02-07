namespace BloodDonationSystem.Domain.Services.Interfaces
{
    public interface IAuth
    {
        string GenerateJwtToken(string email);
        string GenerateRefreshToken(string email);
        string ComputeSha256Hash(string password);
        Task<(bool isValid, string email)> ValidateTokenAsync(string token);
    }
}
