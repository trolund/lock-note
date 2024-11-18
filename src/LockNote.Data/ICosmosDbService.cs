using Microsoft.Azure.Cosmos;

namespace LockNote.Data;

public interface ICosmosDbService
{
    Task<Container> GetContainerAsync(string containerName);
}