using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous
{
    internal class UpdateAnonymousValidator : AbstractValidator<UpdateAnonymousCommand>
    {
        public UpdateAnonymousValidator()
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

            RuleFor(r => r.Files)
                .NotNull().WithMessage("Es necesario adjuntar un documento relacionado a su petición.");
        }
    }
}
