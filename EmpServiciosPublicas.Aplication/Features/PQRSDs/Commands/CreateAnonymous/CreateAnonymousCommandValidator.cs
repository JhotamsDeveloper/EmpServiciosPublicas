using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommandValidator : AbstractValidator<CreateAnonymousCommand>
    {
        public CreateAnonymousCommandValidator()
        {
            RuleFor(r => r.Title)
                .Length(5, 255).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.")
                .NotEmpty().WithMessage("No ha indicado el titulo de la pqrsd.");

            RuleFor(r => r.Descrption)
                .Length(50, 500).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.")
                .NotEmpty().WithMessage("No ha indicado la descripción de la pqrsd.");

            RuleFor(r => r.PQRSDType)
                .Length(3, 20).WithMessage("{PropertyName} tiene {TotalLength} letras. Debe tener una longitud entre {MinLength} y {MaxLength} letras.")
                .NotEmpty().WithMessage("No ha indicado el tipo de la pqrsd.");
        }
    }
}
