using Microsoft.Azure.Cosmos;

namespace LockNote.Data;

public class CosmosDbService : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;

    public CosmosDbService(CosmosDbSettings settings)
    {
        _cosmosClient = new CosmosClient(settings.Endpoint, settings.Key);
        _databaseName = settings.DatabaseName;
    }

    public async Task<Container> GetContainerAsync(string containerName)
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
        return await database.Database.CreateContainerIfNotExistsAsync(containerName, "/partitionKey");
    }
}