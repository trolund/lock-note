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
        
        [HttpPost("{id}")]
        public async Task<ActionResult> GetNote([FromRoute] string id, [FromBody] NoteRequest req)
        {
            var note = await notesService.GetNoteAsync(id, req.Password ?? "");
            
            if(note == null)
            {
                return NotFound();
            }
            
            return Ok(note);
        }
    }
}