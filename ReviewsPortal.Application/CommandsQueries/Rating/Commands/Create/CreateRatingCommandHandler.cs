using MediatR;
using ReviewsPortal.Application.CommandsQueries.Art.Queries;
using ReviewsPortal.Application.CommandsQueries.Rating.Queries.GetUserRating;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Commands.Create;

public class CreateRatingCommandHandler : IRequestHandler<CreateRatingCommand, Domain.Rating>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public CreateRatingCommandHandler(IReviewsPortalDbContext dbContext, IMediator mediator)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Rating> Handle(CreateRatingCommand request, CancellationToken cancellationToken)
    {
        var rating = new Domain.Rating()
        {
            Value = request.Value,
            User = await GetUser(request.UserId, cancellationToken),
            Art = await GetArt(request.ArtId, cancellationToken)
        };
        await _dbContext.Ratings.AddAsync(rating, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return rating;
    }

    private async Task<Domain.User> GetUser(Guid? userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery() { UserId = userId };
        var user = await _mediator.Send(query, cancellationToken);
        return user;
    }

    private async Task<Domain.Art> GetArt(Guid artId, CancellationToken cancellationToken)
    {
        var query = new GetArtQuery() { ArtId = artId };
        var art = await _mediator.Send(query, cancellationToken);
        return art;
    }
}