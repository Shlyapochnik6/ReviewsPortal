using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Category.Queries.GetList;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetCategoriesQueryHandler(IReviewsPortalDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _dbContext.Categories.Select(c => c).ToListAsync(cancellationToken);
        var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
        return categoriesDto;
    }
}