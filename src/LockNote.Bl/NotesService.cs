using LockNote.Data;
using LockNote.Data.Model;

namespace LockNote.Bl;

public class NotesService
{
    private readonly IRepository<Note> _notesRepository;

    public NotesService(IRepository<Note> notesRepository)
    {
        _notesRepository = notesRepository;
    }

    public async Task CreateNoteAsync(Note note)
    {
        await _notesRepository.AddAsync(note);
    }

    public async Task<Note> GetNoteAsync(string id, string partitionKey)
    {
        return await _notesRepository.GetByIdAsync(id, partitionKey);
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await _notesRepository.GetAllAsync("SELECT * FROM c");
    }
}
