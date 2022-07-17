using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create
{
    public class CreateBiddingCommandValidator : AbstractValidator<CreateBiddingCommand>
    {
        public CreateBiddingCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("El título no puede ser null")
                .NotNull().WithMessage("El título no puede no puede estar vacía");

            RuleFor(x => x.Descrption)
                .NotEmpty().WithMessage("la descripción no puede ser null")
                .NotNull().WithMessage("la descripción no puede no puede estar vacía");

            RuleFor(x => x.StartOfTheCall)
                .NotNull().WithMessage("La fecha inicial de la convocatoria no puede ser null")
                .NotEmpty().WithMessage("La fecha inicial de la convocatoria no puede estar vacía");

            RuleFor(x => x.EndOfTheCall)
                .NotNull().WithMessage("La fecha final de la convocatoria no puede ser null")
                .NotEmpty().WithMessage("La fecha final de la convocatoria no puede estar vacía")
                .NotEqual(DateTime.Now);
        }
    }
}
