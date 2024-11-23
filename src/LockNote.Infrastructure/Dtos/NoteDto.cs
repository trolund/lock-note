namespace LockNote.Infrastructure.Dtos;

public class NoteDto
{
    public required int ReadBeforeDelete { get; init; }
    
    public required string Content { get; init; }
    
    public required DateTime CreatedAt { get; init; }
    
    public string? Password { get; set; }
}