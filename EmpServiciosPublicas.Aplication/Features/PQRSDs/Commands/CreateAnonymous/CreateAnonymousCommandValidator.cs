using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Handlers;
using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommandValidator : AbstractValidator<CreateAnonymousCommand>
    {
        public CreateAnonymousCommandValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("No ha ingresado el titulo de la pqrsd.")
                .Length(5, 255).WithMessage("El titulo tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(r => r.Descrption)
                .NotEmpty().WithMessage("No ha ingresado la descripción de la pqrsd.")
                .Length(50, 500).WithMessage("La descripción tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(r => r.Type)
                .NotEmpty().WithMessage("No ha ingresado el tipo de pqrsd.")
                .Must(CustomExtensions.IsEnumName).WithMessage($"No es un valor válida para el tipo de PQRSD, posibles valores. {string.Join(", ", Enum.GetValues(typeof(EnumPQRSD)).Cast<EnumPQRSD>().ToList())}");

            RuleFor(r => r.Files)
                .NotNull().WithMessage("Es necesario adjuntar un documento relacionado a su petición.");
        }
    }
}
