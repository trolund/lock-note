using LockNote.Data;
using LockNote.Data.Model;

namespace LockNote.Bl;

public class NotesService(IRepository<Note> notesRepository)
{
    public async Task CreateNoteAsync(Note note)
    {
        await notesRepository.AddAsync(note);
    }

    public async Task<Note> GetNoteAsync(string id)
    {
        return await notesRepository.GetByIdAsync(id, "Note");
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllAsync("SELECT * FROM c");
    }
}
