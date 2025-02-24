using LockNote.Data.Model;
using LockNote.Data.Repositories;
using LockNote.Infrastructure.Dtos;
using Microsoft.Extensions.Logging;

namespace LockNote.Bl;

public class NotesService(NoteRepository notesRepository, ILogger<NotesService> logger)
{
    public async Task<NoteDto> UpdateNoteAsync(NoteDto note)
    {
        if (note.Id is null)
        {
            throw new ArgumentException("Note id is required");
        }
        
        var noteModel = await notesRepository.GetNoteAsync(note.Id);
        
        noteModel.Content = note.Content;
        // TODO: fix?
        noteModel.ReadBeforeDelete = (note.ReadBeforeDelete ?? 1) - 1;

        if (noteModel.ReadBeforeDelete == 0)
        {
            await DeleteNoteAsync(note.Id);
        }
        
        var updatedNote = await notesRepository.UpdateNoteAsync(noteModel);
        return NoteDto.FromModel(updatedNote);
    }
    
    public async Task<NoteDto?> CreateNoteAsync(NoteDto note)
    {
        var noteModel = new Note
        {
            ReadBeforeDelete = 1,
            Content = note.Content,
            CreatedAt = DateTime.UtcNow
        };

        if (note.Password is null)
        {
            return NoteDto.FromModel(await notesRepository.CreateNoteAsync(noteModel));
        }

        var (salt, hashed) = PasswordHashService.HashPassword(note.Password);

        noteModel.PasswordHash = hashed;
        noteModel.Salt = salt;
        noteModel.Content = Encryption.Encrypt(note.Content, note.Password);
        return NoteDto.FromModel(await notesRepository.CreateNoteAsync(noteModel));
    }

    public async Task<Note?> GetNoteAsync(string id, string password = "")
    {
        var entity = await notesRepository.GetNoteAsync(id);

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
        return await notesRepository.GetAllNotesAsync();
    }
    
    public async Task DeleteNoteAsync(string id)
    {
        await notesRepository.DeleteNoteAsync(id);
    }

    public async Task DeleteAllOverMonthOld()
    {
        var items = await notesRepository.DeleteAllOverMonthOld();
        logger.LogInformation("Deleted {Count} notes", items.Count());
    }
}