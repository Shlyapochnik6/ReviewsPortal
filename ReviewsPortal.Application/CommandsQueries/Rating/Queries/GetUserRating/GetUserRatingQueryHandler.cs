using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Rating.Queries.GetUserRating;

public class GetUserRatingQueryHandler : IRequestHandler<GetUserRatingQuery, Domain.Rating>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetUserRatingQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Rating> Handle(GetUserRatingQuery request, CancellationToken cancellationToken)
    {
        var rating = await _dbContext.Ratings.Include(r => r.Art)
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Art.Id == request.ArtId 
                                      && r.User.Id == request.UserId, cancellationToken);
        return rating;
    }
}