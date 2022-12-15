using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Category.Queries.GetList;

public class GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
    
}