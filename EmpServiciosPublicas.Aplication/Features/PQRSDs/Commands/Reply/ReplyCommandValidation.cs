using EmpServiciosPublicas.Aplication.Constants;
using EmpServiciosPublicas.Aplication.Handlers;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Reply
{
    public class ReplyCommandValidation : AbstractValidator<ReplyCommand>
    {
        public ReplyCommandValidation()
        {
            RuleFor(r => r.IdPqrsd)
                .NotEmpty().WithMessage("El id de la petición es requerida.");

            RuleFor(r => r.Reply)
                .NotEmpty().WithMessage("La respuesta es requerida");
        }
    }
}
