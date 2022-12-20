using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Login;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class UserLoginDto : IMapWith<UserLoginQuery>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public bool RememberMe { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserLoginDto, UserLoginQuery>()
            .ForMember(u => u.Email,
                opt => opt.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                opt => opt.MapFrom(u => u.Password))
            .ForMember(u => u.Remember,
                opt => opt.MapFrom(u => u.RememberMe));
    }
}