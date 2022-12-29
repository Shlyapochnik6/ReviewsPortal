using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Queries.GetAll;

public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, IEnumerable<GetAllCommentsDto>>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetAllCommentsQueryHandler(IMapper mapper, IMediator mediator,
        IReviewsPortalDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<GetAllCommentsDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var comments = await _dbContext.Comments
            .Include(c => c.Review)
            .Include(c => c.User)
            .Where(c => c.Review.Id == request.ReviewId)
            .OrderBy(c => c.CreationDate)
            .ProjectTo<GetAllCommentsDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return comments;
    }
}