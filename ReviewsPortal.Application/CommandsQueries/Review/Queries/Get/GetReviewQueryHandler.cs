using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;

public class GetReviewQueryHandler : IRequestHandler<GetReviewQuery, Domain.Review>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetReviewQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Review> Handle(GetReviewQuery request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews
            .Include(r => r.Art).Include(r => r.Art.Ratings)
            .Include(r => r.Category).Include(r => r.User)
            .Include(r => r.Likes).Include(r => r.Tags)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        if (review == null)
            throw new NullReferenceException($"The review with Id = {request.ReviewId} was not found!");
        return review;
    }
}