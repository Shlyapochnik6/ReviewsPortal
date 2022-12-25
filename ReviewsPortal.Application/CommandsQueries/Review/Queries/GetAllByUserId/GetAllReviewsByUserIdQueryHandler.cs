using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAllByUserId;

public class GetAllReviewsByUserIdQueryHandler : IRequestHandler<GetAllReviewsByUserIdQuery, IEnumerable<GetAllUserReviewsDto>>
{
    private IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetAllReviewsByUserIdQueryHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllUserReviewsDto>> Handle(GetAllReviewsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _dbContext.Reviews.Include(r => r.Art.Ratings)
            .Include(r => r.Likes).Include(r => r.User)
            .Where(r => r.User.Id == request.UserId)
            .ProjectTo<GetAllUserReviewsDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        return reviews;
    }
}