using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDto : IMapWith<Domain.Review>
{
    public string Title { get; set; }
    
    public string ArtName { get; set; }
    
    public string Category { get; set; }
    
    public string Description { get; set; }
    
    public string[] Tags { get; set; }
    
    public string ImageUrl { get; set; }
    
    public string AuthorName { get; set; }

    public int UserRating { get; set; }
    
    public double AverageRating { get; set; }
    
    public int LikesCount { get; set; }
    
    public bool LikeStatus { get; set; }
    
    public int AuthorGrade { get; set; }

    public DateTime CreationDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetReviewDto>()
            .ForMember(r => r.AuthorName,
                opt => opt.MapFrom(r => r.User.UserName))
            .ForMember(r => r.Title,
                opt => opt.MapFrom(r => r.Title))
            .ForMember(r => r.ArtName,
                opt => opt.MapFrom(r => r.Art.ArtName))
            .ForMember(r => r.Category,
                opt => opt.MapFrom(r => r.Category.CategoryName))
            .ForMember(r => r.Description,
                opt => opt.MapFrom(r => r.Description))
            .ForMember(r => r.Tags,
                opt => opt.MapFrom(r => r.Tags.Select(t => t.TagName)))
            .ForMember(r => r.ImageUrl,
                opt => opt.MapFrom(r => r.ImageUrl))
            .ForMember(r => r.AverageRating,
                opt => opt.MapFrom(r => r.Art.AverageRating))
            .ForMember(r => r.LikesCount,
                opt => opt.MapFrom(r => r.Likes.Count(l => l.Status)))
            .ForMember(r => r.AuthorGrade,
                opt => opt.MapFrom(r => r.Grade));
    }
}