using AutoMapper;
using MediatR;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.Registration;

public class UserRegistrationCommand : IRequest, IMapWith<Domain.User>
{
    public string Email { get; set; }
    
    public string Name { get; set; }
    
    public string Password { get; set; }
    
    public bool Remember { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegistrationCommand, Domain.User>()
            .ForMember(u => u.UserName,
                opt => opt.MapFrom(u => u.Name))
            .ForMember(u => u.Email,
                opt => opt.MapFrom(u => u.Email));
    }
}