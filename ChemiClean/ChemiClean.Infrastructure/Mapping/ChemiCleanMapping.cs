using AutoMapper;
using ChemiClean.Core;
using ChemiClean.Core.DTOS;

namespace ChemiClean.Infrastructure.Mapping
{
    public class ChemiCleanMapping : Profile
    {
        public ChemiCleanMapping()
        {
            CreateMap<ProductRequestDto, Product>().ReverseMap();
            CreateMap<ProductResponseDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();
        }
    }
}