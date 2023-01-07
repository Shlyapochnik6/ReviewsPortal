using Algolia.Search.Clients;
using Algolia.Search.Models.Search;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Application.Common.FullTextSearch;

public class AlgoliaClient
{
    private readonly ISearchClient _searchClient;
    private readonly ISearchIndex _searchIndex;

    public AlgoliaClient(string appId, string adminKey, string index)
    {
        _searchClient = new SearchClient(appId, adminKey);
        _searchIndex = _searchClient.InitIndex(index);
    }

    public async Task<List<Guid>> SearchAsync(string query)
    {
        var results = await _searchIndex
            .SearchAsync<Review>(new Query(query));
        var ids = results.Hits.Select(r => r.Id);
        return ids.ToList();
    }

    public async Task AddOrUpdateEntity<T>(T entity) where T : class
    {
        ((dynamic)entity).ObjectID = ConvertIdToObjectID(entity);
        await _searchIndex.SaveObjectAsync(entity);
    }

    public async Task DeleteEntity<T>(T entity) where T : class
    {
        var objectID = ConvertIdToObjectID(entity);
        await _searchIndex.DeleteObjectAsync(objectID);
    }

    public string ConvertIdToObjectID<T>(T entity)
        where T : class
    {
        return ((dynamic)entity).Id.ToString();
    }
}