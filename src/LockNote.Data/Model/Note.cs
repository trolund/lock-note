namespace LockNote.Data.Model;

public class Note() : BaseItem("Note")
{
    public int ReadBeforeDelete { get; init; } = 1;
    
    public required string Content { get; init; }
    
    public required DateTime CreatedAt { get; init; }
    
    public string? PasswordHash { get; set; }
    public byte[]? Salt { get; set; }
}
