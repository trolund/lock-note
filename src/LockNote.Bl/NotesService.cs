using LockNote.Data;
using LockNote.Data.Model;
using LockNote.Infrastructure.Dtos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace LockNote.Bl;

public class NotesService(IRepository<Note> notesRepository, ILogger<NotesService> logger)
{
    public async Task<NoteDto> CreateNoteAsync(NoteDto note)
    {
        var noteModel = new Note
        {
            ReadBeforeDelete = 1,
            Content = note.Content,
            CreatedAt = DateTime.UtcNow
        };

        if (note.Password is null)
        {
            return NoteDto.FromModel(await notesRepository.AddAsync(noteModel));
        }

        var (salt, hashed) = PasswordHashService.HashPassword(note.Password);

        noteModel.PasswordHash = hashed;
        noteModel.Salt = salt;
        noteModel.Content = Encryption.Encrypt(note.Content, note.Password);
        return NoteDto.FromModel(await notesRepository.AddAsync(noteModel));
    }

    public async Task<Note?> GetNoteAsync(string id, string password = "")
    {
        var entity = await notesRepository.GetByIdAsync(id, "Note");

        // if a password is set but not correct, return null
        if (entity?.PasswordHash != null &&
            !PasswordHashService.VerifyPassword(password, entity.Salt!, entity.PasswordHash))
        {
            return new Note()
            {
                Content = "Enter the correct password to view the note", Id = "passwordIncorrect",
                CreatedAt = DateTime.UtcNow
            };
        }

        // if a password is set and correct, decrypt the content
        if (entity?.PasswordHash != null)
        {
            entity.Content = Encryption.Decrypt(entity.Content, password);
        }

        return entity;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllAsync(new QueryDefinition("SELECT * FROM c"));
    }

    public async Task DeleteAllOverMonthOld()
    {
        // all notes where CreatedAt is more then a month ago
        var query = new QueryDefinition(
            $"SELECT * FROM c WHERE c.CreatedAt < '{DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ")}'");

        var items = (await notesRepository.GetAllAsync(query)).ToList();

        foreach (var item in items)
        {
            await notesRepository.DeleteAsync(item.Id, "Note");
        }

        logger.LogInformation("Deleted {Count} notes", items.Count);
    }
}