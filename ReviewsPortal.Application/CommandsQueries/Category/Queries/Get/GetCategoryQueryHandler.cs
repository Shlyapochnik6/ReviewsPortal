using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Category.Queries.Get;

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, Domain.Category>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetCategoryQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(c => c.CategoryName == request.CategoryName, cancellationToken);
        if (category == null)
            throw new NullReferenceException($"No category with name: {request.CategoryName} was found");
        return category;
    }
}