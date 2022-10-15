using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.Posts.Commands.Delete
{
    internal class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("No ha ingresado el {PropertyName} del post.")
                .NotEmpty().WithMessage("No ha ingresado el {PropertyName} del post.");
        }
    }
}
