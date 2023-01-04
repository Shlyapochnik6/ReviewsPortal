using System.Collections;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetMostRated;

public class GetMostRatedReviewsQueryHandler : IRequestHandler<GetMostRatedReviewsQuery, IEnumerable<GetAllReviewsDto>>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetMostRatedReviewsQueryHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllReviewsDto>> Handle(GetMostRatedReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _dbContext.Reviews
            .Include(r => r.Likes)
            .Include(r => r.Art)
            .Include(r => r.Art.Ratings)
            .Include(r => r.Tags)
            .Include(r => r.Images!)
            .OrderByDescending(r => r.Art.AverageRating)
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        return reviews;
    }
}