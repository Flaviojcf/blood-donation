using BloodDonationSystem.Application.Commands.CreateDonation;
using FluentValidation;

namespace BloodDonationSystem.Application.Validators.Donor
{
    public class CreateDonationCommandValidator : AbstractValidator<CreateDonationCommand>
    {
        public CreateDonationCommandValidator()
        {
            RuleFor(command => command.QuantityML)
                .GreaterThanOrEqualTo(420).WithMessage("A quantidade deve ser maior ou igual a 420ml.")
                .LessThanOrEqualTo(470).WithMessage("A quantidade deve ser menor ou igual a 470ml.");

            RuleFor(command => command.DonorId)
                .NotEmpty().WithMessage("O ID do doador não deve estar vazio.");
        }
    }
}
