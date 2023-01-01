using MediatR;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.User.Commands.SetLikesCount;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Delete;

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public DeleteReviewCommandHandler(IMediator mediator, IReviewsPortalDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        _dbContext.Reviews.Remove(review);
        await _dbContext.SaveChangesAsync(cancellationToken);
        await ChangeUserLikesCount(review.User.Id, cancellationToken);
        return Unit.Value;
    }

    private async Task ChangeUserLikesCount(Guid? userId, CancellationToken cancellationToken)
    {
        var command = new SetUserLikesCountCommand() { UserId = userId };
        await _mediator.Send(command, cancellationToken);
    }
    
    private async Task<Domain.Review> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var query = new GetReviewQuery() { ReviewId = reviewId };
        var review = await _mediator.Send(query, cancellationToken);
        return review;
    }
}