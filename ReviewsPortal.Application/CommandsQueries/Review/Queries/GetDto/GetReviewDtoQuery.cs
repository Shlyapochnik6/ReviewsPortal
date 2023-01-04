using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDtoQuery : IRequest<GetReviewDto>
{
    public Guid ReviewId { get; set; }

    public Guid? UserId { get; set; }

    public GetReviewDtoQuery(Guid reviewId, Guid? userId)
    {
        ReviewId = reviewId;
        UserId = userId;
    }
}