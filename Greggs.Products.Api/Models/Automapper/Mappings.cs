using AutoMapper;
using Greggs.Products.Api.Models.DTO.Product;
using Greggs.Products.Api.Models.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        {
            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Product, ProductWithCurrencyDTO>()
                .ForMember(dest => dest.CurrencyCode, opt => opt.Ignore())
                .ForMember(dest => dest.CurrencyAdjustedPrice, opt => opt.Ignore());

            CreateMap<ProductDTO, ProductWithCurrencyDTO>()
                .ForMember(dest => dest.CurrencyCode, opt => opt.Ignore()) // Set dynamically
                .ForMember(dest => dest.CurrencyAdjustedPrice, opt => opt.Ignore());
        }
    }

}
