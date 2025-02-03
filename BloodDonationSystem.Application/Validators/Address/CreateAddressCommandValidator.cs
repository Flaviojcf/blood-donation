using BloodDonationSystem.Application.Commands.CreateAddress;
using FluentValidation;

namespace BloodDonationSystem.Application.Validators.Address
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {
            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Rua é obrigatória.")
                .MaximumLength(100).WithMessage("Rua não deve exceder 100 caracteres.");

            RuleFor(x => x.Number)
                .GreaterThan(0).WithMessage("Número deve ser maior que 0.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Cidade é obrigatória.")
                .MaximumLength(50).WithMessage("Cidade não deve exceder 50 caracteres.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("Estado é obrigatório.")
                .MaximumLength(50).WithMessage("Estado não deve exceder 50 caracteres.");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("CEP deve estar no formato 12345-678.");

            RuleFor(x => x.DonorId)
               .NotEmpty().WithMessage("O Id do doardo é obrigatório.");
        }
    }
}
