using LockNote.Bl;
using LockNote.Data.Model;
using LockNote.Infrastructure.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LockNote.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController(NotesService notesService) : ControllerBase
    {
        
        [HttpPost]
        public async Task<ActionResult> CreateNote(NoteDto noteDto)
        {
            await notesService.CreateNoteAsync(new Note()
            {
                Id = Guid.NewGuid().ToString(), Content = noteDto.Content, CreatedAt = DateTime.Now,
                PasswordHash = noteDto.PasswordHash, ReadBeforeDelete = noteDto.ReadBeforeDelete
            });
            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult> GetNotes()
        {
            var notes = await notesService.GetAllNotesAsync();
            return Ok(notes);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetNote(string id)
        {
            var note = await notesService.GetNoteAsync(id);
            return Ok(note);
        }
    }
}