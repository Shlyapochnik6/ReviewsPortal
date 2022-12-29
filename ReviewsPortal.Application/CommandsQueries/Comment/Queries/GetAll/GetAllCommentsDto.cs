using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Queries.GetAll;

public class GetAllCommentsDto : IMapWith<Domain.Comment>
{
    public string AuthorName { get; set; }
    
    public int LikesCount { get; set; }
    
    public DateTime CreationDate { get; set; }
    
    public string Text { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Comment, GetAllCommentsDto>()
            .ForMember(c => c.LikesCount,
                opt => opt.MapFrom(c => c.User.LikesCount))
            .ForMember(c => c.AuthorName,
                opt => opt.MapFrom(c => c.User.UserName))
            .ForMember(c => c.CreationDate,
                opt => opt.MapFrom(c => c.CreationDate))
            .ForMember(c => c.Text,
                opt => opt.MapFrom(c => c.Text));
    }
}