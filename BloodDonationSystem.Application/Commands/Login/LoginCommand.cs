using BloodDonationSystem.Domain.Records;
using MediatR;

namespace BloodDonationSystem.Application.Commands.Login
{
    public class LoginCommand : IRequest<LoginRecord>
    {
        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
