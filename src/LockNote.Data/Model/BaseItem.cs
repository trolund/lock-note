using Newtonsoft.Json;

namespace LockNote.Data.Model;

public class BaseItem
{
    [JsonProperty("id")] 
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("partitionKey")] 
    protected string PartitionKey { get; init; }
}