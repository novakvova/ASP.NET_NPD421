using AutoMapper;
using WebATB.Data.Entities;
using WebATB.Models.Product;

namespace WebATB.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductCreateModel, ProductEntity>();
    }
}
