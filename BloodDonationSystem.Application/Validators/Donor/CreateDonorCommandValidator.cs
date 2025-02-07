using BloodDonationSystem.Application.Commands.CreateDonor;
using FluentValidation;

namespace BloodDonationSystem.Application.Validators.Donor
{
    public class CreateDonorCommandValidator : AbstractValidator<CreateDonorCommand>
    {
        public CreateDonorCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Nome é obrigatório.")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email é obrigatório.")
                .EmailAddress().WithMessage("Email deve ser um endereço válido.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("A senha é obrigatória.");


            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Data de nascimento é obrigatória.")
                .LessThan(DateTime.Now).WithMessage("Data de nascimento deve ser menor que a data atual.");

            RuleFor(x => x.GenderType)
                .IsInEnum().WithMessage("Tipo de gênero inválido.");

            RuleFor(x => x.Weight)
                .GreaterThan(0).WithMessage("Peso deve ser maior que 0.");

            RuleFor(x => x.BloodType)
                .IsInEnum().WithMessage("Tipo sanguíneo inválido.");

            RuleFor(x => x.RhFactorType)
                .IsInEnum().WithMessage("Fator Rh inválido.");
        }
    }
}
