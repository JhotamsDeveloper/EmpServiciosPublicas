using FluentValidation;

namespace EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Update
{
    public class UpdateBiddingCommandValidator : AbstractValidator<UpdateBiddingCommand>
    {
        public UpdateBiddingCommandValidator()
        {
            RuleFor(r => r.Id)
                .NotNull().WithMessage("No ha ingresado el Id de la convocatoria.")
                .NotEmpty().WithMessage("No ha ingresado el Id de la convocatoria.");

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
