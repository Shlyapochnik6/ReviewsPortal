using AutoMapper;
using MediatR;
using ReviewsPortal.Application.CommandsQueries.Like.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Rating.Queries.GetUserRating;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetDto;

public class GetReviewDtoQueryHandler : IRequestHandler<GetReviewDtoQuery, GetReviewDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetReviewDtoQueryHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _mediator = mediator;
    }
    
    public async Task<GetReviewDto> Handle(GetReviewDtoQuery request, CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        var reviewDto = _mapper.Map<GetReviewDto>(review);
        reviewDto.LikeStatus = await GetLikeStatus(request.ReviewId, request.UserId, cancellationToken);
        reviewDto.UserRating = await GetUserRating(review.Art.Id, request.UserId, cancellationToken);
        return reviewDto;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var query = new GetReviewQuery(reviewId);
        var review = await _mediator.Send(query, cancellationToken);
        return review;
    }

    private async Task<bool> GetLikeStatus(Guid reviewId, Guid? userId,
        CancellationToken cancellationToken)
    {
        var query = new GetLikeQuery(reviewId, userId);
        var like = await _mediator.Send(query, cancellationToken);
        return like?.Status ?? false;
    }

    private async Task<int> GetUserRating(Guid artId, Guid? userId,
        CancellationToken cancellationToken)
    {
        var query = new GetUserRatingQuery(artId, userId);
        var userRating = await _mediator.Send(query, cancellationToken);
        return userRating == null ? 1 : userRating.Value;
    }
}