using AutoMapper;
using WebATB.Data.Entities;
using WebATB.Models.Category;

namespace WebATB.Mappers;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryEntity,CategoryItemModel>()
            .ForMember(x=>x.Image, 
                opt => opt.MapFrom(x=>x.Image!=null ? $"/images/{x.Image}" : null));

        CreateMap<CategoryCreateModel, CategoryEntity>()
            .ForMember(x=>x.Image, opt => opt.Ignore());
    }
}
