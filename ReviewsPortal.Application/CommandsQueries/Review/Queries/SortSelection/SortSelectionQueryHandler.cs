using AutoMapper;
using MediatR;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetAll;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetMostRated;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetRecentlyAdded;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.SortSelection;

public class SortSelectionQueryHandler : IRequestHandler<SortSelectionQuery, IEnumerable<GetAllReviewsDto>>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public SortSelectionQueryHandler(IReviewsPortalDbContext dbContext,
        IMediator mediator)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllReviewsDto>> Handle(SortSelectionQuery request, CancellationToken cancellationToken)
    {
        var sorting = request.Sorting;
        var reviews = sorting switch
        {
            SortParameters.RecentlyAdded =>
                await GetRecentlyAddedReviews(cancellationToken),
            SortParameters.MostRated =>
                await GetMostRatedReviews(cancellationToken),
            _ => await GetAllReviews(cancellationToken)
        };
        if (request.Tag != null)
            reviews = reviews.Where(r => r.Tags.Contains(request.Tag)).ToList();
        return reviews;
    }

    private async Task<IEnumerable<GetAllReviewsDto>> GetMostRatedReviews(CancellationToken cancellationToken)
    {
        var query = new GetMostRatedReviewsQuery();
        var reviews = await _mediator.Send(query, cancellationToken);
        return reviews;
    }
    
    private async Task<IEnumerable<GetAllReviewsDto>> GetRecentlyAddedReviews(CancellationToken cancellationToken)
    {
        var query = new GetRecentlyAddedQuery();
        var reviews = await _mediator.Send(query, cancellationToken);
        return reviews;
    }
    
    private async Task<IEnumerable<GetAllReviewsDto>> GetAllReviews(CancellationToken cancellationToken)
    {
        var query = new GetAllReviewsQuery();
        var reviews = await _mediator.Send(query, cancellationToken);
        return reviews;
    }
}