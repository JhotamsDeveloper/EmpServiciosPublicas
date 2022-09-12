using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("No ha ingresado el titulo de la categoria.")
                .Length(5, 20).WithMessage("La categoria tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Descrption)
                .NotEmpty().WithMessage("No ha ingresado la descripción de la categoria.")
                .Length(50, 500).WithMessage("La descripción tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} carateres.");

            RuleFor(r => r.Icono)
                .NotNull().WithMessage("Es necesario adjuntar el icono relacionado a su petición.");
        }
    }
}
