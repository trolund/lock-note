using LockNote.Data.Base;
using LockNote.Data.Exceptions;
using LockNote.Data.Model;
using Microsoft.Azure.Cosmos;

namespace LockNote.Data.Repositories;

public class NoteRepository(IRepository<Note> notesRepository)
{
    public async Task<Note> UpdateNoteAsync(Note note)
    {
        var noteModel = await notesRepository.GetByIdAsync(note.Id);

        noteModel.Content = note.Content;
        noteModel.ReadBeforeDelete = note.ReadBeforeDelete - 1;

        return await notesRepository.UpdateAsync(note.Id, noteModel);
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        return await notesRepository.AddAsync(note);
    }

    public async Task<Note> GetNoteAsync(string id)
    {
        var dao = await notesRepository.GetByIdAsync(id);

        if (dao is null)
        {
            throw new DalException("Note not found");
        }

        return dao;
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        return await notesRepository.GetAllAsync(new QueryDefinition("SELECT * FROM c"));
    }

    // delete one note by id
    public async Task DeleteNoteAsync(string id)
    {
        await notesRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Note>> DeleteAllOverMonthOld()
    {
        // all notes where CreatedAt is more then a month ago
        var query = new QueryDefinition(
            $"SELECT * FROM c WHERE c.CreatedAt < '{DateTime.UtcNow.AddMonths(-1):yyyy-MM-ddTHH:mm:ss.ffffffZ}'");

        var items = (await notesRepository.GetAllAsync(query)).ToList();

        foreach (var item in items)
        {
            await notesRepository.DeleteAsync(item.Id);
        }

        return items;
    }
}