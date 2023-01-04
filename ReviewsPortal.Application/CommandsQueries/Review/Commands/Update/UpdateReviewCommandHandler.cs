using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ReviewsPortal.Application.CommandsQueries.Category.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Review.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Tag.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetList;
using ReviewsPortal.Application.Common.Clouds.Firebase;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Update;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly FirebaseCloud _firebase;
    private readonly IReviewsPortalDbContext _dbContext;

    public UpdateReviewCommandHandler(IMapper mapper, IMediator mediator, FirebaseCloud firebase,
        IReviewsPortalDbContext dbContext)
    {
        _mapper = mapper;
        _mediator = mediator;
        _firebase = firebase;
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
        var query = new GetReviewQuery(reviewId);
        return await _mediator.Send(query, cancellationToken);
    }

    private async Task<Domain.Category> GetCategory(string categoryName,
        CancellationToken cancellationToken)
    {
        var query = new GetCategoryQuery(categoryName);
        return await _mediator.Send(query, cancellationToken);
    }
    
    private async Task<List<Domain.Tag>> GetTags(string[] tags, CancellationToken cancellationToken)
    {
        var query = new GetTagsQuery(tags);
        return (await _mediator.Send(query, cancellationToken)).ToList();
    }

    private async Task CreateTags(string[] tags, CancellationToken cancellationToken)
    {
        var command = new CreateTagCommand(tags);
        await _mediator.Send(command, cancellationToken);
    }

    private async Task<List<Domain.Image>> UpdateImages(IEnumerable<IFormFile> files,
        string folderName)
    {
        var imageData = await _firebase.UpdateFiles(files, folderName);
        return _mapper.Map<IEnumerable<ImageData>, List<Domain.Image>>(imageData);
    }

    private async Task<Domain.Review> GetUpdatedReview(UpdateReviewCommand request, 
        CancellationToken cancellationToken)
    {
        var review = await GetReview(request.ReviewId, cancellationToken);
        var updatedReview = _mapper.Map(request, review);
        updatedReview.Tags = await GetTags(request.Tags, cancellationToken);
        updatedReview.Category = await GetCategory(request.CategoryName, cancellationToken);
        updatedReview.Art.ArtName = request.ArtName;
        updatedReview.Images = await UpdateImages(request.Images, 
            review.Images?.FirstOrDefault()?.FolderName ?? Guid.NewGuid().ToString());
        return updatedReview;
    }
}