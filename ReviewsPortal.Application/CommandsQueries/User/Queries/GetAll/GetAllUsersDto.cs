using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.GetAll;

public class GetAllUsersDto : IMapWith<Domain.User>
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, GetAllUsersDto>()
            .ForMember(u => u.Id,
                o => o.MapFrom(u => u.Id))
            .ForMember(u => u.UserName,
                o => o.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                o => o.MapFrom(u => u.Email));
    }
}