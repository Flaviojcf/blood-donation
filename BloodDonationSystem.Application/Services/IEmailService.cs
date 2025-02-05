namespace BloodDonationSystem.Application.Services
{
    public interface IEmailService
    {
        Task SendAsync(string email, string subject, string message);
    }
}
