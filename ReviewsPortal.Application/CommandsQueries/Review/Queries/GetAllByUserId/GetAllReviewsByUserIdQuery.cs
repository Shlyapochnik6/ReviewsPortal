using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAllByUserId;

public class GetAllReviewsByUserIdQuery : IRequest<IEnumerable<GetAllUserReviewsDto>>
{
    public Guid UserId { get; set; }
}