using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotepadController : ControllerBase
    {
        static private List<Notepad> notepads = new List<Notepad>
        {
            new Notepad
            {
                Id = 1,
                Title = "First Note",
                Content = "This is the first note",
                Date = "2021-09-01",
                isPinned = false
            },
            new Notepad
            {
                Id = 2,
                Title = "Second Note",
                Content = "This is the second note",
                Date = "2021-09-02",
                isPinned = false
            },
            new Notepad
            {
                Id = 3,
                Title = "Third Note",
                Content = "This is the third note",
                Date = "2021-09-03",
                isPinned = false
            }
        };

        [HttpGet]
        public ActionResult<List<Notepad>> Get()
        {
            return notepads;
        }

        [HttpGet("{id}")]
        public ActionResult<Notepad> Get(int id)
        {
            var notepad = notepads.FirstOrDefault(n => n.Id == id);
            if (notepad == null)
            {
                return NotFound();
            }
            return notepad;
        }

        [HttpPost]
        public ActionResult<Notepad> Post(Notepad notepad)
        {
            if (notepad == null)
            {
                return BadRequest();
            }

            // Check if a Notepad with the same title already exists
            if (notepads.Any(n => n.Title == notepad.Title))
            {
                return StatusCode(StatusCodes.Status409Conflict); // Conflict - title already exists
            }

            notepad.Id = notepads.Max(n => n.Id) + 1;
            notepads.Add(notepad);
            return CreatedAtAction(nameof(Get), new { id = notepad.Id }, notepad);
        }

        [HttpPut("{id}")]
        public IActionResult Put (int id, Notepad notepad)
        {
            if (id != notepad.Id)
            {
                return BadRequest();
            }

            var existingNotepad = notepads.FirstOrDefault(n => n.Id == id);
            if (existingNotepad == null)
            {
                return NotFound();
            }

            existingNotepad.Title = notepad.Title;
            existingNotepad.Content = notepad.Content;
            existingNotepad.Date = notepad.Date;
            existingNotepad.isPinned = notepad.isPinned;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var notepad = notepads.FirstOrDefault(n => n.Id == id);
            if (notepad == null)
            {
                return NotFound();
            }
            notepads.Remove(notepad);
            return NoContent();
        }
    }
}
