using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Handlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("No ha ingresado el Id de la pqrsd.")
                .NotEmpty().WithMessage("No ha ingresado el Id de la pqrsd.");

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
