using MediatR;
using Microsoft.AspNetCore.SignalR;
using ReviewsPortal.Application.CommandsQueries.Comment.Queries.Get;

namespace ReviewsPortal.Application.Common.Hubs;

public class CommentHub : Hub
{
    private readonly IMediator _mediator;

    public CommentHub(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task ConnectToGroup(Guid reviewId)
    {
        var connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, reviewId.ToString());
    }

    public async Task SendComment(Guid reviewId, Guid commentId)
    {
        var query = new GetCommentQuery(commentId);
        var comment = await _mediator.Send(query);
        await Clients.Group(reviewId.ToString()).SendAsync("GetComment", comment);
    }
}