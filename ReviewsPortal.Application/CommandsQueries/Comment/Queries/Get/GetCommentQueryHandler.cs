using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Queries.Get;

public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, GetCommentDto>
{
    private readonly IMapper _mapper;
    private readonly IReviewsPortalDbContext _dbContext;

    public GetCommentQueryHandler(IMapper mapper, IReviewsPortalDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    
    public async Task<GetCommentDto> Handle(GetCommentQuery request, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
        var commentDto = _mapper.Map<GetCommentDto>(comment);
        return commentDto;
    }
}