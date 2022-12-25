using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetMostRated;

public class GetMostRatedReviewsQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
}