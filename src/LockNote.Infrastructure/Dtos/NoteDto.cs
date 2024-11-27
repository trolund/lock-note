using LockNote.Data.Model;

namespace LockNote.Infrastructure.Dtos;

public class NoteDto
{
    public string? Id { get; init; }
    public int? ReadBeforeDelete { get; init; }
    public required string Content { get; init; }
    public DateTime? CreatedAt { get; init; }
    public string? Password { get; set; }
    public static NoteDto FromModel(Note note)
    {
        return new NoteDto
        {
            Id = note.Id,
            ReadBeforeDelete = note.ReadBeforeDelete,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
    }
}