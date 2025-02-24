using LockNote.Bl;
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
            var note = await notesService.CreateNoteAsync(noteDto);

            if (note is null)
            {
                return NotFound($"Note with id: {noteDto.Id} could not be found");
            }
            
            return Ok(note);
        }
        
        [HttpGet]
        public async Task<ActionResult> GetNotes()
        {
            var notes = await notesService.GetAllNotesAsync();
            return Ok(notes);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetNote(string id, [FromQuery] string password = "")
        {
            var note = await notesService.GetNoteAsync(id, password);
            
            if(note == null)
            {
                return NotFound();
            }
            
            return Ok(note);
        }
    }
}