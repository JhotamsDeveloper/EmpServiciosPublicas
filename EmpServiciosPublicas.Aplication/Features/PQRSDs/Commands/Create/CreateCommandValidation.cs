using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Handlers;
using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create
{
    public class CreateCommandValidation : AbstractValidator<CreateCommand>
    {
        public CreateCommandValidation()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("No ha ingresado el primer nombre.")
                .Length(5, 100).WithMessage("El primer nombre tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Surnames)
                .NotEmpty().WithMessage("No ha ingresado los apellidos.")
                .Length(5, 100).WithMessage("Los apellidos tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.DocumentType)
                .NotEmpty().WithMessage("No ha ingresado el tipo de documento.")
                .Length(5, 100).WithMessage("El tipo de documento tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");


            RuleFor(r => r.DocumentNumer)
                .NotEmpty().WithMessage("No ha ingresado el número de documento.")
                .Length(5, 100).WithMessage("El número de documento tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("No ha ingresado el email.")
                .EmailAddress().WithMessage("El email no contine un formato válido")
                .Length(5, 100).WithMessage("El email tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.CellPhone)
                .NotEmpty().WithMessage("No ha ingresado el número de celular.")
                .Length(5, 13).WithMessage("El número de celular tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("No ha ingresado el titulo de la pqrsd.")
                .Length(5, 255).WithMessage("El titulo tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Descrption)
                .NotEmpty().WithMessage("No ha ingresado la descripción de la pqrsd.")
                .Length(50, 500).WithMessage("La descripción tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Type)
                .NotEmpty().WithMessage("No ha ingresado el tipo de pqrsd.")
                .Must(CustomExtensions.IsEnumName).WithMessage($"No es un valor válida para el tipo de PQRSD, posibles valores. {string.Join(", ", Enum.GetValues(typeof(EnumPQRSD)).Cast<EnumPQRSD>().ToList())}");

            RuleFor(r => r.Files)
                .NotNull().WithMessage("Es necesario adjuntar un documento relacionado a su petición.");
        }
    }

}
