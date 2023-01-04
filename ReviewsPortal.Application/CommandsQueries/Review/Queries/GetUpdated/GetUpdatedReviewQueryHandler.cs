using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.Common.Clouds.Firebase;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;

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
        var review = await GetReview(request, cancellationToken);
        if (review == null)
            throw new NullReferenceException($"The review with id: {request.ReviewId} was not found");
        var reviewDto = _mapper.Map<GetUpdatedReviewDto>(review);
        if (review.Images != null)
            reviewDto.ImagesData = _mapper.Map<List<Image>, ImageData[]>(review.Images);
        return reviewDto;
    }

    private async Task<Domain.Review> GetReview(GetUpdatedReviewQuery request, CancellationToken cancellationToken)
    {
        var review = await _dbContext.Reviews
            .Include(r => r.Tags)
            .Include(r => r.Art)
            .Include(r => r.Category)
            .Include(r => r.Images)
            .FirstOrDefaultAsync(r => r.Id == request.ReviewId, cancellationToken);
        return review;
    }
}