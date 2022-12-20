using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Art.Queries;

public class GetArtQueryHandler : IRequestHandler<GetArtQuery, Domain.Art>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetArtQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Art> Handle(GetArtQuery request, CancellationToken cancellationToken)
    {
        var art = await _dbContext.Arts
            .FirstOrDefaultAsync(a => a.Id == request.ArtId, cancellationToken);
        if (art == null)
            throw new NullReferenceException($"No art with id: {request.ArtId} was found");
        return art;
    }
}