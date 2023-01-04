using MediatR;
using ReviewsPortal.Application.CommandsQueries.Art.Queries;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Art.Commands.UpdateAverageRating;

public class UpdateAverageRatingCommandHandler : IRequestHandler<UpdateAverageRatingCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public UpdateAverageRatingCommandHandler(IReviewsPortalDbContext dbContext, IMediator mediator)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdateAverageRatingCommand request, CancellationToken cancellationToken)
    {
        var art = await GetArt(request.ArtId, cancellationToken);
        _dbContext.Arts.Update(art);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task<Domain.Art> GetArt(Guid artId, CancellationToken cancellationToken)
    {
        var query = new GetArtQuery(artId);
        var art = await _mediator.Send(query, cancellationToken);
        art.AverageRating = CalculateAverageRating(art);
        return art;
    }

    private double CalculateAverageRating(Domain.Art art)
    {
        var averageRating = (art.Ratings.Select(r => r.Value)).Average();
        return averageRating;
    }
}