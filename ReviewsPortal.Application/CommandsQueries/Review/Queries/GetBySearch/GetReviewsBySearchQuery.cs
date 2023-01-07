using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetBySearch;

public class GetReviewsBySearchQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
    public IEnumerable<Guid> Ids { get; set; }

    public GetReviewsBySearchQuery(IEnumerable<Guid> ids)
    {
        Ids = ids;
    }
}