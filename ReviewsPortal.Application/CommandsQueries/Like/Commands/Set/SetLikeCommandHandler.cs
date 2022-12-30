using MediatR;
using ReviewsPortal.Application.CommandsQueries.Like.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Like.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.User.Commands.SetLikesCount;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Like.Commands.Set;

public class SetLikeCommandHandler : IRequestHandler<SetLikeCommand, Guid>
{
    private readonly IReviewsPortalDbContext _dbContext;
    private readonly IMediator _mediator;

    public SetLikeCommandHandler(IReviewsPortalDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public async Task<Guid> Handle(SetLikeCommand request, CancellationToken cancellationToken)
    {
        var likeId = await UpdateLikeStatus(request, cancellationToken);
        var review = await GetReview(request.ReviewId, cancellationToken);
        await SetUserLikesCount(review.User.Id, cancellationToken);
        return likeId;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId, 
        CancellationToken cancellationToken)
    {
        var query = new GetReviewQuery() { ReviewId = reviewId };
        return await _mediator.Send(query, cancellationToken);
    }

    private async Task<Domain.Like> GetLike(SetLikeCommand request, 
        CancellationToken cancellationToken)
    {
        var query = new GetLikeQuery()
        {
            UserId = request.UserId,
            ReviewId = request.ReviewId
        };
        return await _mediator.Send(query, cancellationToken) ??
               await CreateLike(request, cancellationToken);
    }

    private async Task<Guid> UpdateLikeStatus(SetLikeCommand request,
        CancellationToken cancellationToken)
    {
        var like = await GetLike(request, cancellationToken);
        like.Status = request.Status;
        _dbContext.Likes.Update(like);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return like.Id;
    }

    private async Task<Domain.Like> CreateLike(SetLikeCommand request, 
        CancellationToken cancellationToken)
    {
        var command = new CreateLikeCommand()
        {
            UserId = request.UserId,
            ReviewId = request.ReviewId,
            Status = request.Status
        };
        return await _mediator.Send(command, cancellationToken);
    }

    private async Task SetUserLikesCount(Guid userId,
        CancellationToken cancellationToken)
    {
        var query = new SetUserLikesCountCommand() { UserId = userId };
        await _mediator.Send(query, cancellationToken);
    }
}