using LockNote.Data;
using LockNote.Data.Model;
using LockNote.Infrastructure.Dtos;

namespace LockNote.Bl;

public class NotesService(IRepository<Note> notesRepository)
{
    public async Task CreateNoteAsync(NoteDto note)
    {
        var noteModel = new Note
        {
            ReadBeforeDelete = note.ReadBeforeDelete,
            Content = note.Content,
            CreatedAt = note.CreatedAt
        };
        
        if(note.Password == null)
        {
            await notesRepository.AddAsync(noteModel);
            return;
        }

        var (salt, hashed) = PasswordHashService.HashPassword(note.Password);

        noteModel.PasswordHash = hashed;
        noteModel.Salt = salt;
        await notesRepository.AddAsync(noteModel);
    }

    public async Task<Note?> GetNoteAsync(string id, string password = "")
    {
        var entity = await notesRepository.GetByIdAsync(id, "Note");
        
        // if a password is set but not correct, return null
        if(entity?.PasswordHash != null && !PasswordHashService.VerifyPassword(password, entity.Salt!, entity.PasswordHash))
        {
            return null;
        }   
        
        
        return entity;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllAsync("SELECT * FROM c");
    }
}
