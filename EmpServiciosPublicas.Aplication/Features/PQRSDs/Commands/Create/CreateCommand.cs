﻿using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create
{
    public class CreateCommand : IRequest<string>
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? Surnames { get; set; }
        public string? DocumentType { get; set; }
        public string? DocumentNumer { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? CellPhone { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Descrption { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public ICollection<IFormFile>? Files { get; set; }
    }
}
