using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.SortSelection;

public class SortSelectionQuery : IRequest<IEnumerable<GetAllReviewsDto>>
{
    public string? Sorting { get; set; }
    
    public string? Tag { get; set; }

    public SortSelectionQuery(string? sorting, string? tag)
    {
        Sorting = sorting;
        Tag = tag;
    }
}