using Microsoft.Azure.Cosmos;

namespace LockNote.Data;

public class CosmosDbService(string? connectionString, CosmosDbSettings settings) : ICosmosDbService
{
    private readonly CosmosClient _cosmosClient = connectionString != null ? new CosmosClient(connectionString,
        new CosmosClientOptions
        {
            ApplicationName = "LockNote",
            ConnectionMode = ConnectionMode.Gateway,
            LimitToEndpoint = true
        }) : throw new ArgumentException("Connection string is required");

    private readonly string _databaseName = settings.DatabaseName;
    private readonly string _containerName = settings.ContainerName;

    public async Task<Container> GetContainerAsync()
    {
        return _cosmosClient.GetContainer(_databaseName, _containerName);
        var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName, 4000,
            new RequestOptions() { PriorityLevel = PriorityLevel.High });
        return await database.Database.CreateContainerIfNotExistsAsync(_containerName, "/partitionKey", 4000);
    }
}