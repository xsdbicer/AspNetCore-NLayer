using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;

namespace NLayer.Service.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>();
            CreateMap<ProductAddDto, Product>();

            CreateMap<Product, ProductWithCategoryDTO>();
        }
    }
}
