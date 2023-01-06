using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Application.Common.FullTextSearch;

public class AlgoliaDbSync
{
    private readonly IReviewsPortalDbContext _dbContext;
    private readonly AlgoliaClient _client;

    public AlgoliaDbSync(IReviewsPortalDbContext dbContext,
        AlgoliaClient client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task Synchronize()
    {
        var entitiesEntries = _dbContext.ChangeTracker
            .Entries<Review>().ToList();
        foreach (var entry in entitiesEntries)
            await ExecuteClientOperation(entry);
    }

    private async Task ExecuteClientOperation(EntityEntry<Review> entityEntry)
    {
        await LoadRelatedEntities(entityEntry.Entity);
        switch (entityEntry.State)
        {
            case EntityState.Added or EntityState.Modified or EntityState.Unchanged:
                await _client.AddOrUpdateEntity(entityEntry.Entity);
                break;
            case EntityState.Deleted:
                await _client.DeleteEntity(entityEntry.Entity);
                break;
        }
    }

    private async Task LoadRelatedEntities(Review review)
    {
        await _dbContext.Reviews.Entry(review)
            .Collection(r => r.Comments).LoadAsync();
        await _dbContext.Reviews.Entry(review)
            .Collection(r => r.Likes).LoadAsync();
        await _dbContext.Reviews.Entry(review)
            .Collection(r => r.Tags).LoadAsync();
        await _dbContext.Reviews.Entry(review)
            .Reference(r => r.Art).LoadAsync();
        await _dbContext.Reviews.Entry(review)
            .Reference(r => r.Category).LoadAsync();
    }
}