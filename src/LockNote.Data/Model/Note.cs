namespace LockNote.Data.Model;

public class Note : BaseItem
{
    public Note()
    {
        PartitionKey = "Note";
    }
    
    public int ReadBeforeDelete { get; init; } = 1;
    
    public required string Content { get; init; }
    
    public required DateTime CreatedAt { get; init; }
    
    public string? PasswordHash { get; set; }
}
