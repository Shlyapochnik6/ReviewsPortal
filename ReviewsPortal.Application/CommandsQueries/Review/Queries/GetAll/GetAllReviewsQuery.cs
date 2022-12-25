using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAll;

public class GetAllReviewsQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
    
}