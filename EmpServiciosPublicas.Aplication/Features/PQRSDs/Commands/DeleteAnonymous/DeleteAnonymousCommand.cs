﻿using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.DeleteAnonymous
{
    public class DeleteAnonymousCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
