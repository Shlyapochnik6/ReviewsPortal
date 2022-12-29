using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Like.Queries.Get;

public class GetLikeQueryHandler : IRequestHandler<GetLikeQuery, Domain.Like>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetLikeQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Like> Handle(GetLikeQuery request, CancellationToken cancellationToken)
    {
        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.Review.Id == request.ReviewId 
                                      && l.User.Id == request.UserId, cancellationToken);
        return like;
    }
}