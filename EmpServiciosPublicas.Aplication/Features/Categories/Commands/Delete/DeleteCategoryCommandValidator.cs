using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.Categories.Commands.Delete
{

    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("No ha ingresado el {PropertyName} de la pqrsd.")
                .NotEmpty().WithMessage("No ha ingresado el {PropertyName} de la pqrsd.");
        }
    }
}
