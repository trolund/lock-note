namespace LockNote.Data;

using Microsoft.Azure.Cosmos;

public class CosmosRepository<T> : IRepository<T> where T : class
{
    private readonly Container _container;

    public CosmosRepository(ICosmosDbService cosmosDbService, string containerName)
    {
        _container = cosmosDbService.GetContainerAsync(containerName).GetAwaiter().GetResult();
    }

    public async Task<T> GetByIdAsync(string id, string partitionKey)
    {
        try
        {
            var response = await _container.ReadItemAsync<T>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task<IEnumerable<T>> GetAllAsync(string query)
    {
        var queryIterator = _container.GetItemQueryIterator<T>(new QueryDefinition(query));
        var results = new List<T>();

        while (queryIterator.HasMoreResults)
        {
            var response = await queryIterator.ReadNextAsync();
            results.AddRange(response);
        }

        return results;
    }

    public async Task AddAsync(T entity)
    {
        await _container.CreateItemAsync(entity);
    }

    public async Task UpdateAsync(string id, T entity)
    {
        await _container.UpsertItemAsync(entity, new PartitionKey(id));
    }

    public async Task DeleteAsync(string id, string partitionKey)
    {
        await _container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey));
    }
}
