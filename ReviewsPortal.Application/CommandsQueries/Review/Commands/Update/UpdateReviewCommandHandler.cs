using AutoMapper;
using MediatR;
using ReviewsPortal.Application.CommandsQueries.Category.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Tag.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetList;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Update;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public UpdateReviewCommandHandler(IMapper mapper, IMediator mediator,
        IReviewsPortalDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        await CreateTags(request.Tags, cancellationToken);
        var updatedReview = await GetUpdatedReview(request, cancellationToken);
        _dbContext.Reviews.Update(updatedReview);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task<Domain.Review> GetReview(Guid reviewId, CancellationToken cancellationToken)
    {
        var query = new GetReviewQuery() { ReviewId = reviewId };
        return await _mediator.Send(query, cancellationToken);
    }

    private async Task<Domain.Category> GetCategory(string categoryName,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryQuery() { CategoryName = categoryName };
        return await _mediator.Send(query, cancellationToken);
    }
    
    private async Task<List<Domain.Tag>> GetTags(string[] tags, CancellationToken cancellationToken)
    {
        var query = new GetTagsQuery(tags);
        return (await _mediator.Send(query, cancellationToken)).ToList();
    }

    private async Task CreateTags(string[] tags, CancellationToken cancellationToken)
    {
        var command = new CreateTagCommand() { Tags = tags };
        await _mediator.Send(command, cancellationToken);
    }

    private async Task<Domain.Review> GetUpdatedReview(UpdateReviewCommand request, 
        CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        var updatedReview = _mapper.Map(request, review);
        updatedReview.Tags = await GetTags(request.Tags, cancellationToken);
        updatedReview.Category = await GetCategory(request.CategoryName, cancellationToken);
        updatedReview.Art.ArtName = request.ArtName;
        return updatedReview;
    }
}