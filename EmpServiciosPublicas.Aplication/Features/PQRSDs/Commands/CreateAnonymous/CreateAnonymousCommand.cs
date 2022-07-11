﻿using MediatR;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous
{
    public class CreateAnonymousCommand: IRequest<string>
    {
        public string Title { get; set; } = string.Empty;
        public string Descrption { get; set; } = string.Empty;
        public string PQRSDType { get; set; } = string.Empty;

    }
}