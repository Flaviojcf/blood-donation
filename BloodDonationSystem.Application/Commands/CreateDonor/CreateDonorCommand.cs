using BloodDonationSystem.Domain.Enums;
using MediatR;

namespace BloodDonationSystem.Application.Commands.CreateDonor
{
    public class CreateDonorCommand : IRequest<Guid>
    {
        public CreateDonorCommand(string fullName, string email, DateTime birthDate, GenderType genderType, decimal weight, BloodType bloodType, RhFactorType rhFactorType)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            GenderType = genderType;
            Weight = weight;
            BloodType = bloodType;
            RhFactorType = rhFactorType;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderType GenderType { get; set; }
        public decimal Weight { get; set; }
        public BloodType BloodType { get; set; }
        public RhFactorType RhFactorType { get; set; }
    }
}
