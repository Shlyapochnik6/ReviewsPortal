using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Like.Queries.Get;

public class GetLikeQuery : IRequest<Domain.Like>
{
    public Guid ReviewId { get; set; }

    public Guid? UserId { get; set; }

    public GetLikeQuery(Guid reviewId, Guid? userId)
    {
        ReviewId = reviewId;
        UserId = userId;
    }
}