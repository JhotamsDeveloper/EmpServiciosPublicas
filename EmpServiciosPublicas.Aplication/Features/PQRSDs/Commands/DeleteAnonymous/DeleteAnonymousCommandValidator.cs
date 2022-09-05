using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous
{
    public class DeleteAnonymousCommandValidator : AbstractValidator<DeleteAnonymousCommand>
    {
        public DeleteAnonymousCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("No ha ingresado el {PropertyName} de la pqrsd.")
                .NotEmpty().WithMessage("No ha ingresado el {PropertyName} de la pqrsd.");
        }
    }
}
