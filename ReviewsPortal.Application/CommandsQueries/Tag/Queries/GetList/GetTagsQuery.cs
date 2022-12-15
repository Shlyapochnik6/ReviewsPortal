using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetList;

public class GetTagsQuery : IRequest<IEnumerable<Domain.Tag>>
{
    public string[] Tags { get; set; }

    public GetTagsQuery(string[] tags)
    {
        Tags = tags;
    }
}