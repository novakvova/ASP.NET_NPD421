using AutoMapper;
using WebATB.Data.Entities.Idenity;
using WebATB.Models.Account;
namespace WebATB.Mappers;

public class AccoutMapper : Profile
{
    public AccoutMapper()
    {
        CreateMap<RegisterViewModel, UserEntity>()
            .ForMember(x=>x.UserName, opt=> opt.MapFrom(x=>x.Email))
            .ForMember(x=>x.Image, opt => opt.Ignore());
    }
}
