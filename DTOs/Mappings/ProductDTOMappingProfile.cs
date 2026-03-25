using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings;

public class ProductDTOMappingProfile : Profile
{
    public ProductDTOMappingProfile()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductUpdateRequestDTO>().ReverseMap();
        CreateMap<Product, ProductUpdateResponseDTO>().ReverseMap();
    }
}