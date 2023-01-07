using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.Search;

public class SearchReviewsQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
    public string Search { get; set; }

    public SearchReviewsQuery(string search)
    {
        Search = search;
    }
}