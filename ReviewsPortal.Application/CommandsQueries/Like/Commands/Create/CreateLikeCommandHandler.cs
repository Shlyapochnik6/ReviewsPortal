using MediatR;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Like.Commands.Create;

public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Domain.Like>
{
    private readonly IReviewsPortalDbContext _dbContext;
    private readonly IMediator _mediator;

    public CreateLikeCommandHandler(IReviewsPortalDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }
    
    public async Task<Domain.Like> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        var like = new Domain.Like()
        {
            Status = request.Status,
            User = await GetUser(request.UserId, cancellationToken),
            Review = await GetReview(request.ReviewId, cancellationToken)
        };
        await _dbContext.Likes.AddAsync(like, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return like;
    }
    
    private async Task<Domain.User> GetUser(Guid? userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery() { UserId = userId };
        return await _mediator.Send(query, cancellationToken);
    }
    
    private async Task<Domain.Review> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var query = new GetReviewQuery() { ReviewId = reviewId };
        return await _mediator.Send(query, cancellationToken);
    }
}