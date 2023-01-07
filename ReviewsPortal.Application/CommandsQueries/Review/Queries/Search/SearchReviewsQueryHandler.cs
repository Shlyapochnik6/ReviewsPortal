using MediatR;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.GetBySearch;
using ReviewsPortal.Application.Common.FullTextSearch;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.Search;

public class SearchReviewsQueryHandler : IRequestHandler<SearchReviewsQuery, IEnumerable<GetAllReviewsDto>>
{
    private readonly IMediator _mediator;
    private readonly AlgoliaClient _algoliaClient;

    public SearchReviewsQueryHandler(IMediator mediator,
        AlgoliaClient algoliaClient)
    {
        _mediator = mediator;
        _algoliaClient = algoliaClient;
    }
    
    public async Task<IEnumerable<GetAllReviewsDto>> Handle(SearchReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviewsIds = await SearchReviews(request.Search);
        var reviewsDtos = await GetAllReviews(reviewsIds, cancellationToken);
        return reviewsDtos;
    }

    private async Task<IEnumerable<GetAllReviewsDto>> GetAllReviews(List<Guid> ids, CancellationToken cancellationToken)
    {
        var query = new GetReviewsBySearchQuery(ids);
        var reviews = await _mediator.Send(query, cancellationToken);
        return reviews.ToList();
    }

    private async Task<List<Guid>> SearchReviews(string search)
    {
        var reviewsIds = await _algoliaClient.SearchAsync(search);
        return reviewsIds;
    }
}