using AutoMapper;
using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Bidding.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.Categories.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.Posts.Commands.Update;
using EmpServiciosPublicas.Aplication.Features.Posts.Queries.GetAllPosts;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.Create;
using EmpServiciosPublicas.Aplication.Features.PQRSDs.Commands.CreateAnonymous;
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

            CreateMap<Post, CreatePostCommand>().ReverseMap();
            CreateMap<Post, UpdatePostCommand>().ReverseMap();
            CreateMap<Post, GetAllPostsMV>().ReverseMap();

            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();

            CreateMap<Bidding, CreateBiddingCommand>().ReverseMap();
            CreateMap<Bidding, UpdateBiddingCommand>().ReverseMap();

            CreateMap<TenderProposal, CreateTenderProposalCommand>().ReverseMap();
            CreateMap<TenderProposal, UpdateTenderProposalCommand>().ReverseMap();
            CreateMap<TenderProposal, DeleteTenderProposalCommand>().ReverseMap();
        }
    }
}
