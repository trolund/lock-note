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
    }
}