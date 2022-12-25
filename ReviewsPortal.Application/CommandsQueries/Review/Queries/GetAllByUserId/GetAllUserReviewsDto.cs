using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAllByUserId;

public class GetAllUserReviewsDto : IMapWith<Domain.Review>
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }

    public string ArtName { get; set; }
    
    public int AuthorGrade { get; set; }
    
    public string ImageUrl { get; set; }

    public int LikeCount { get; set; }

    public DateTime CreationDate { get; set; }

    public double AverageRating { get; set; }

    public string Category { get; set; }
    
    public List<string> Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetAllUserReviewsDto>()
            .ForMember(r => r.Id,
                opt => opt.MapFrom(r => r.Id))
            .ForMember(r => r.Title,
                opt => opt.MapFrom(r => r.Title))
            .ForMember(r => r.ArtName,
                opt => opt.MapFrom(r => r.Art.ArtName))
            .ForMember(r => r.AuthorGrade,
                opt => opt.MapFrom(r => r.Grade))
            .ForMember(r => r.ImageUrl,
                opt => opt.MapFrom(r => r.ImageUrl))
            .ForMember(r => r.LikeCount,
                opt => opt.MapFrom(r => r.Likes.Count))
            .ForMember(r => r.CreationDate,
                opt => opt.MapFrom(r => r.CreationDate))
            .ForMember(r => r.AverageRating,
                opt => opt.MapFrom(r => r.Art.Ratings
                    .Select(a => a.Value).DefaultIfEmpty().Average()))
            .ForMember(r => r.Category,
                opt => opt.MapFrom(r => r.Category))
            .ForMember(r => r.Tags,
                opt => opt.MapFrom(r => r.Tags.Select(t => t.TagName)));
    }
}