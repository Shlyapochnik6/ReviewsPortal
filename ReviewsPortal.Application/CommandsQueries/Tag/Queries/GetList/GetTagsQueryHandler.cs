using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetList;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<Domain.Tag>>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetTagsQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Domain.Tag>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _dbContext.Tags.Where(t => request.Tags.Any(i => i == t.TagName))
            .ToListAsync(cancellationToken);
        return tags;
    }
}