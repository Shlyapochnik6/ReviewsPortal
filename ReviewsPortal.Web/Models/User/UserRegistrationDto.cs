using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.User.Commands.Registration;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class UserRegistrationDto : IMapWith<UserRegistrationCommand>
{
    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }

    public string Confirm { get; set; }
    
    public bool RememberMe { get; set; }
    

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserRegistrationDto, UserRegistrationCommand>()
            .ForMember(u => u.Email,
                opt => opt.MapFrom(u => u.Email))
            .ForMember(u => u.Name,
                opt => opt.MapFrom(u => u.UserName))
            .ForMember(u => u.Password,
                opt => opt.MapFrom(u => u.Password))
            .ForMember(u => u.Remember,
                opt => opt.MapFrom(u => u.RememberMe));
    }
}