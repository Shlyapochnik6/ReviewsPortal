using AutoMapper;
using MediatR;
using ReviewsPortal.Application.CommandsQueries.Art.Queries;
using ReviewsPortal.Application.CommandsQueries.Category.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Tag.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetList;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IReviewsPortalDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateReviewCommandHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _mapper = mapper;
    }
    
    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        await CreateTags(request.Tags);
        var reviewId = await CreateReview(request, cancellationToken);
        return reviewId;
    }

    private async Task CreateTags(string[] tags)
    {
        var command = new CreateTagCommand() { Tags = tags };
        await _mediator.Send(command);
    }

    private async Task<Domain.User> GetUser(Guid? userId)
    {
        var query = new GetUserQuery() { UserId = userId };
        var user = await _mediator.Send(query);
        return user;
    }

    private async Task<Domain.Category> GetCategory(string categoryName)
    {
        var query = new GetCategoryQuery() { CategoryName = categoryName };
        var category = await _mediator.Send(query);
        return category;
    }

    private async Task<List<Domain.Tag>> GetTags(string[] tags)
    {
        var query = new GetTagsQuery(tags);
        var reviewTags = await _mediator.Send(query);
        return reviewTags.ToList();
    }

    private async Task<Guid> CreateReview(CreateReviewCommand request,
        CancellationToken cancellationToken)
    {
        var review = _mapper.Map<Domain.Review>(request);
        review.User = await GetUser(request.UserId);
        review.Art = new Domain.Art() { ArtName = request.ArtName };
        review.Category = await GetCategory(request.Category);
        review.CreationDate = DateTime.UtcNow;
        review.Tags = await GetTags(request.Tags);
        await _dbContext.Reviews.AddAsync(review, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return review.Id;
    }
}