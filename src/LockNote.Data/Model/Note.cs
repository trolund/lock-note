namespace LockNote.Data.Model;

public class Note : BaseItem
{
    public Note()
    {
        PartitionKey = "Note";
    }
    
    public required string Content { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required bool IsDeleted { get; init; }
}
