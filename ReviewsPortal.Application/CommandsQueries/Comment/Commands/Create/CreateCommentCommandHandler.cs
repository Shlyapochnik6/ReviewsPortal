using AutoMapper;
using MediatR;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Commands.Create;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public CreateCommentCommandHandler(IMapper mapper, IMediator mediator,
        IReviewsPortalDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Domain.Comment>(request);
        comment.User = await GetUser(request.UserId);
        comment.Review = await GetReview(request.ReviewId);
        await _dbContext.Comments.AddAsync(comment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return comment.Id;
    }

    private async Task<Domain.User> GetUser(Guid? userId)
    {
        var query = new GetUserQuery(userId);
        return await _mediator.Send(query);
    }

    private async Task<Domain.Review> GetReview(Guid reviewId)
    {
        var query = new GetReviewQuery(reviewId);
        return await _mediator.Send(query);
    }
}