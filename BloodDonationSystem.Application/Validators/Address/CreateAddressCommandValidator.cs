using BloodDonationSystem.Application.Commands.CreateAddress;
using FluentValidation;

namespace BloodDonationSystem.Application.Validators.Address
{
    public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
    {
        public CreateAddressCommandValidator()
        {

            RuleFor(x => x.Number)
                .GreaterThan(0).WithMessage("Número da residência deve ser maior que 0.");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("CEP é obrigatório.")
                .Matches(@"^\d{5}-\d{3}$").WithMessage("CEP deve estar no formato 12345-678.");

            RuleFor(x => x.DonorId)
               .NotEmpty().WithMessage("O ID do doador não deve estar vazio.");
        }
    }
}
