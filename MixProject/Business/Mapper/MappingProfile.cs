using AutoMapper;
using MixProject.Entity;
using MixProject.Entity.Dto;

namespace MixProject.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Product,ProductDTO>().ReverseMap();
            
        }
    }
}
