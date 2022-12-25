using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetRecentlyAdded;

public class GetRecentlyAddedQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
}