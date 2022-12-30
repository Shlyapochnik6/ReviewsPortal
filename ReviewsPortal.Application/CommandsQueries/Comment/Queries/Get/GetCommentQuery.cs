using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Queries.Get;

public class GetCommentQuery : IRequest<GetCommentDto>
{
    public Guid CommentId { get; set; }
}