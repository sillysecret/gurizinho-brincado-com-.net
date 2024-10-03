using AutoMapper;
using gurizinho.DTOs;
using gurizinho.models;

namespace gurizinho.MappingProfile
{
    public class MappingProfile : Profile 
    {
        public MappingProfile() 
        { 
            CreateMap<ProdutoCreateDTO,Produto>().ReverseMap();
        } 
    }
}
