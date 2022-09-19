using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create;
using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete
{
    public class UpdatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public UpdatePostCommandValidator()
        {
            RuleFor(r => r.CategoryId)
                .NotEmpty().WithMessage("No ha ingresado el id de la categoría.");

            RuleFor(r => r.Title)
                .NotEmpty().WithMessage("No ha ingresado el titulo del post.")
                .Length(5, 255).WithMessage("El titulo tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");

            RuleFor(r => r.Descrption)
                .NotEmpty().WithMessage("No ha ingresado la descripción del post.")
                .Length(50, 500).WithMessage("La descripción tiene {TotalLength} carateres. Debe tener una longitud entre {MinLength} y {MaxLength} letras.");
        }
    }
}

