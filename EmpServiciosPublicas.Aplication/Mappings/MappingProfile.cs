﻿using AutoMapper;
using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.Posts.Models;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Reply;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.UpdateAnonymous;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Queries.GetPqrsdByTypePqrsd;
using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Delete;
using EmpServiciosPublicas.Aplication.Features.TenderProposals.Commands.Update;
using EmpServiciosPublicos.Domain;

namespace EmpServiciosPublicas.Aplication.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PQRSD, PqrsdMv>().ReverseMap();
            CreateMap<PQRSD, UpdateCommand>().ReverseMap();
            CreateMap<PQRSD, CreateCommand>().ReverseMap();
            CreateMap<PQRSD, CreateAnonymousCommand>().ReverseMap();
            CreateMap<PQRSD, UpdateAnonymousCommand>().ReverseMap();
            CreateMap<PQRSD, ReplyCommand>().ReverseMap();

            CreateMap<Post, CreatePostCommand>().ReverseMap();
            CreateMap<Post, UpdatePostCommand>().ReverseMap();
            CreateMap<Post, GetAllPostsMV>().ReverseMap();
            CreateMap<Post, PostResponse>()
                .ForMember(x => x.State, y => y
                .MapFrom(a => a.Availability == true ? "Activado" : "Inactivo"));

            CreateMap<Storage, StorageMV>().ReverseMap();

            CreateMap<Bidding, CreateBiddingCommand>().ReverseMap();
            CreateMap<Bidding, UpdateBiddingCommand>().ReverseMap();

            CreateMap<TenderProposal, CreateTenderProposalCommand>().ReverseMap();
            CreateMap<TenderProposal, UpdateTenderProposalCommand>().ReverseMap();
            CreateMap<TenderProposal, DeleteTenderProposalCommand>().ReverseMap();

            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<CategoryMV, Category>().ReverseMap()
                .ForMember(x => x.Icono!, y => y.MapFrom(a => a.RouteIcono))
                .ForMember(x => x.Name, y => y.MapFrom(a => a.Title));
        }
    }
}
