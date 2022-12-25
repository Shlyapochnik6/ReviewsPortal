using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries;

public class GetAllReviewsDto : IMapWith<Domain.Review>
{
    public Guid ReviewId { get; set; }
    
    public string Title { get; set; }

    public string ArtName { get; set; }
    
    public int AuthorGrade { get; set; }

    public int LikesCount { get; set; }

    public DateTime CreationDate { get; set; }

    public double AverageRating { get; set; }

    public List<string> Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetAllReviewsDto>()
            .ForMember(r => r.ReviewId,
                opt => opt.MapFrom(r => r.Id))
            .ForMember(r => r.Title,
                opt => opt.MapFrom(r => r.Title))
            .ForMember(r => r.ArtName,
                opt => opt.MapFrom(r => r.Art.ArtName))
            .ForMember(r => r.AuthorGrade,
                opt => opt.MapFrom(r => r.Grade))
            .ForMember(r => r.LikesCount,
                opt => opt.MapFrom(r => r.Likes.Count(l => l.Status == true)))
            .ForMember(r => r.CreationDate,
                opt => opt.MapFrom(r => r.CreationDate))
            .ForMember(r => r.AverageRating,
                opt => opt.MapFrom(r => r.Art.AverageRating))
            .ForMember(r => r.Tags,
                opt => opt.MapFrom(r => r.Tags.Select(t => t.TagName)));
    }
}