namespace LockNote.Data.Model;

public class Note
{
    public string Id { get; set; } // Unique ID for Cosmos DB
    public string PartitionKey { get; set; } // Partition Key
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
