using NanoidDotNet;
using Newtonsoft.Json;

namespace LockNote.Data.Model;

public abstract class BaseItem(string partitionKey)
{
    [JsonProperty("id")] 
    public string Id { get; set; } = Nanoid.Generate(Nanoid.Alphabets.LettersAndDigits, 10);

    [JsonProperty("partitionKey")] 
    public string PartitionKey { get; set; } = partitionKey;
}