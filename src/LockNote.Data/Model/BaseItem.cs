using Newtonsoft.Json;

namespace LockNote.Data.Model;

public abstract class BaseItem(string partitionKey)
{
    [JsonProperty("id")] 
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("partitionKey")] 
    public string PartitionKey { get; set; } = partitionKey;
}