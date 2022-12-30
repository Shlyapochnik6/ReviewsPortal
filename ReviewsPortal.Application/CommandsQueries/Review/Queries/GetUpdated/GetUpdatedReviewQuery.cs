using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetUpdated;

public class GetUpdatedReviewQuery : IRequest<GetUpdatedReviewDto>
{
    public Guid ReviewId { get; set; }
}