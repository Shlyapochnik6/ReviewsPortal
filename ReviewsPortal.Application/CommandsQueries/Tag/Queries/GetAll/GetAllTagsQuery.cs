using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetAll;

public class GetAllTagsQuery : IRequest<IEnumerable<TagDto>>
{
    
}