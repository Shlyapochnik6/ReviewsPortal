using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ReviewsPortal.Application.CommandsQueries.Art.Queries;
using ReviewsPortal.Application.CommandsQueries.Category.Queries.Get;
using ReviewsPortal.Application.CommandsQueries.Tag.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Tag.Queries.GetList;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Common.Clouds.Firebase;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IReviewsPortalDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly FirebaseCloud _firebase;

    public CreateReviewCommandHandler(IReviewsPortalDbContext dbContext,
        IMapper mapper, IMediator mediator, FirebaseCloud firebase)
    {
        _dbContext = dbContext;
        _mediator = mediator;
        _mapper = mapper;
        _firebase = firebase;
    }
    
    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        await CreateTags(request.Tags);
        var reviewId = await CreateReview(request, cancellationToken);
        return reviewId;
    }

    private async Task CreateTags(string[] tags)
    {
        var command = new CreateTagCommand(tags);
        await _mediator.Send(command);
    }

    private async Task<Domain.User> GetUser(Guid? userId)
    {
        var query = new GetUserQuery(userId);
        var user = await _mediator.Send(query);
        return user;
    }

    private async Task<Domain.Category> GetCategory(string categoryName)
    {
        var query = new GetCategoryQuery(categoryName);
        var category = await _mediator.Send(query);
        return category;
    }

    private async Task<List<Domain.Tag>> GetTags(string[] tags)
    {
        var query = new GetTagsQuery(tags);
        var reviewTags = await _mediator.Send(query);
        return reviewTags.ToList();
    }

    private async Task<List<Domain.Image>> UploadImages(IEnumerable<IFormFile> files)
    {
        var imagesData = await _firebase
            .UploadFiles(files, Guid.NewGuid().ToString());
        return _mapper.Map<IEnumerable<ImageData>, List<Domain.Image>>(imagesData);
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
        if (request.Images != null)
            review.Images = await UploadImages(request.Images);
        await _dbContext.Reviews.AddAsync(review, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return review.Id;
    }
}