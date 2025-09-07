using AutoMapper;
using WebATB.Data.Entities;
using WebATB.Models.Product;

namespace WebATB.Mappers;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductCreateModel, ProductEntity>();
        CreateMap<ProductEntity, ProductItemModel>()
            .ForMember(opt=> opt.CategoryName, prop => prop.MapFrom(x=>x.Category.Name));
    }
}
