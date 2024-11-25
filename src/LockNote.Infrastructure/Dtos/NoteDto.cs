namespace LockNote.Infrastructure.Dtos;

public class NoteDto
{
    public int? ReadBeforeDelete { get; init; }
    
    public required string Content { get; init; }
    
    public DateTime? CreatedAt { get; init; }
    
    public string? Password { get; set; }
}