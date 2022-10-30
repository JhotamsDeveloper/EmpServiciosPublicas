using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Create
{
    public class CreateTenderProposalCommandValidator : AbstractValidator<CreateTenderProposalCommand>
    {
        public CreateTenderProposalCommandValidator()
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

            RuleFor(r => r.File)
                .NotNull().WithMessage("Es necesario adjuntar un documento relacionado a su petición.");
        }
    }
}
