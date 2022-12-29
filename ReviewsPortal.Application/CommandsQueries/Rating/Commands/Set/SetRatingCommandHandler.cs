using MediatR;
using ReviewsPortal.Application.CommandsQueries.Art.Commands.UpdateAverageRating;
using ReviewsPortal.Application.CommandsQueries.Rating.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Rating.Queries.GetUserRating;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Commands.Set;

public class SetRatingCommandHandler : IRequestHandler<SetRatingCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public SetRatingCommandHandler(IReviewsPortalDbContext dbContext, IMediator mediator)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(SetRatingCommand request, CancellationToken cancellationToken)
    {
        var artId = await GetArtId(request.ReviewId, cancellationToken);
        var userRating = await GetUserRating(request, artId, cancellationToken);
        await UpdateAverageRating(artId, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
    
    private async Task<Guid> GetArtId(Guid reviewId, CancellationToken cancellationToken)
    {
        var query = new GetReviewQuery() { ReviewId = reviewId };
        var review = await _mediator.Send(query, cancellationToken);
        var artId = review.Art.Id;
        return artId;
    }

    private async Task<Domain.Rating> GetUserRating(SetRatingCommand request, Guid artId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserRatingQuery()
        {
            ArtId = artId,
            UserId = request.UserId
        };
        var userRating = await _mediator.Send(query, cancellationToken) 
                         ?? await CreateRating(request, artId, cancellationToken);
        userRating.Value = request.Value;
        return userRating;
    }
    
    private async Task UpdateAverageRating(Guid artId, CancellationToken cancellationToken)
    {
        var command = new UpdateAverageRatingCommand() { ArtId = artId };
        await _mediator.Send(command, cancellationToken);
    }

    private async Task<Domain.Rating> CreateRating(SetRatingCommand request, Guid artId,
        CancellationToken cancellationToken)
    {
        var command = new CreateRatingCommand()
        {
            ArtId = artId,
            UserId = request.UserId,
            Value = request.Value
        };
        var rating = await _mediator.Send(command, cancellationToken);
        return rating;
    }
}