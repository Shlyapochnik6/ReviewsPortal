using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Queries.GetAll;

public class GetAllCommentsQuery : IRequest<IEnumerable<GetAllCommentsDto>>
{
    public Guid ReviewId { get; set; }

    public GetAllCommentsQuery(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}