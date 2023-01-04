using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;

public class GetReviewQuery : IRequest<Domain.Review>
{
    public Guid ReviewId { get; set; }

    public GetReviewQuery(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}