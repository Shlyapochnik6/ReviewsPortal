using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetUpdated;

public class GetUpdatedReviewQueryHandler : IRequestHandler<GetUpdatedReviewQuery, GetUpdatedReviewDto>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetUpdatedReviewQueryHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<GetUpdatedReviewDto> Handle(GetUpdatedReviewQuery request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Art)
            .Include(r => r.Category)
            .Include(r => r.Images!)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        if (review == null)
            throw new NullReferenceException($"The review with id: {request.ReviewId} was not found");
        return _mapper.Map<GetUpdatedReviewDto>(review);
    }
}