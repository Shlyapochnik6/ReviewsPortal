using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.User.Commands.SetRole;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models.User;

public class SetRoleDto : IMapWith<SetUserRoleCommand>
{
    public Guid UserId { get; set; }
    
    public string Role { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRoleDto, SetUserRoleCommand>();
    }
}