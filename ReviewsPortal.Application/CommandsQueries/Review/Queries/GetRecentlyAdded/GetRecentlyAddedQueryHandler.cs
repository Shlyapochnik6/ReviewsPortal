using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetRecentlyAdded;

public class GetRecentlyAddedQueryHandler : IRequestHandler<GetRecentlyAddedQuery, IEnumerable<GetAllReviewsDto>>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetRecentlyAddedQueryHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllReviewsDto>> Handle(GetRecentlyAddedQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _dbContext.Reviews
            .Include(r => r.Likes).Include(r => r.Art)
            .Include(r => r.Art.Ratings).Include(r => r.Tags)
            .OrderByDescending(r => r.CreationDate)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        return reviews;
    }
}