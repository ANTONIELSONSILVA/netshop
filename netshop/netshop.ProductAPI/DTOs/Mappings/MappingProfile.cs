using AutoMapper;
using netshop.ProductAPI.models;
namespace netshop.ProductAPI.DTOs.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Product, ProductDTO>().ReverseMap();
    }
}
