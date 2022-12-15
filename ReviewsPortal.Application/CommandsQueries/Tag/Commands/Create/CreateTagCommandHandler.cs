using MediatR;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.Tag.Commands.Create;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Unit>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public CreateTagCommandHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var uniqueTags = request.Tags
            .Except(_dbContext.Tags.Select(t => t.TagName));
        await AddTagsToDatabase(uniqueTags, cancellationToken);
        return Unit.Value;
    }

    private async Task AddTagsToDatabase(IEnumerable<string> tags, CancellationToken cancellationToken)
    {
        foreach (var name in tags)
        {
            var tag = new Domain.Tag() { TagName = name };
            await _dbContext.Tags.AddAsync(tag, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}