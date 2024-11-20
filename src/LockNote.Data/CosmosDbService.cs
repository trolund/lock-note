using Microsoft.Azure.Cosmos;

namespace LockNote.Data;

public class CosmosDbService(CosmosDbSettings settings) : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient = new(settings.Endpoint, settings.Key,
        new()
        {
            ApplicationName = "LockNote",
            ConnectionMode = ConnectionMode.Gateway,
            LimitToEndpoint = true
        });

    private readonly string _databaseName = settings.DatabaseName;
    private readonly string _containerName = settings.ContainerName;

    public async Task<Container> GetContainerAsync()
    {
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName, 4000,
            new RequestOptions() { PriorityLevel = PriorityLevel.High });
        return await database.Database.CreateContainerIfNotExistsAsync(_containerName, "/partitionKey", 4000);
    }
}