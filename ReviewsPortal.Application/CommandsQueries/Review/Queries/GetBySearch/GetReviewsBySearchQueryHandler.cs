using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetBySearch;

public class GetReviewsBySearchQueryHandler : IRequestHandler<GetReviewsBySearchQuery, IEnumerable<GetAllReviewsDto>>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetReviewsBySearchQueryHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllReviewsDto>> Handle(GetReviewsBySearchQuery request, CancellationToken cancellationToken)
    {
        var reviewsDtos = await _dbContext.Reviews
            .Include(r => r.Likes)
            .Include(r => r.Art)
            .Include(r => r.Art.Ratings)
            .Include(r => r.Tags)
            .Include(r => r.Images!)
            .Where(r => request.Ids.Contains(r.Id))
            .ProjectTo<GetAllReviewsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return reviewsDtos;
    }
}