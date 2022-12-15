using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Category.Queries.Get;

public class GetCategoryQuery : IRequest<Domain.Category>
{
    public string CategoryName { get; set; }
}