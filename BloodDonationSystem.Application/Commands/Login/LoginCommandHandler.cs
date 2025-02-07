using BloodDonationSystem.Domain.Records;
using BloodDonationSystem.Domain.Repositories;
using BloodDonationSystem.Domain.Services.Interfaces;
using MediatR;

namespace BloodDonationSystem.Application.Commands.Login
{
    public class LoginCommandHandler(IAuthService authService, IDonorRepository donorRepository) : IRequestHandler<LoginCommand, LoginRecord>
    {
        private readonly IAuthService _authService = authService;
        private readonly IDonorRepository _donorRepository = donorRepository;
        public async Task<LoginRecord> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _authService.HashPassword(request.Password);

            var donor = await _donorRepository.GetByEmailAndPasswordAsync(request.Email, passwordHash);

            if (donor == null)
            {
                return null;
            }

            var token = _authService.GenerateJwtToken(donor.Email);

            var loginRecord = new LoginRecord(donor.Email, token);

            return loginRecord;
        }
    }
}
