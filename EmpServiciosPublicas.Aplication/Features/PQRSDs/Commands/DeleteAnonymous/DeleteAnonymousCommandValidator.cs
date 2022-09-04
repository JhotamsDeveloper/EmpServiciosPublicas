using EmpServiciosPublicas.Aplication.Handlers;
using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous
{
    public class DeleteAnonymousCommandValidator : AbstractValidator<DeleteAnonymousCommand>
    {
        public DeleteAnonymousCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("No ha indicado el {PropertyName} de la pqrsd.")
                .NotEmpty().WithMessage("No ha indicado el {PropertyName} de la pqrsd.");
        }
    }
}
