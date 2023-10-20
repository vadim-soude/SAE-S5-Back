using System.Collections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopAPI.Models;

namespace WorkshopAPI.Controllers;

[Route("api/event")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly WorkshopContext _context;

    public EventsController(WorkshopContext context)
    {
        _context = context;
    }

    // GET: api/event
    [HttpGet]
    public async Task<ActionResult<IEnumerable>> GetEvents()
    {
      var sportEvents = await _context.Events
          .Include(sportEvent => sportEvent.IdSportNavigation)
          .Include(sportEvent => sportEvent.IdLevelNavigation)
          .Include(sportEvent => sportEvent.IdUserNavigation)
          .ToListAsync();
      
      return sportEvents;
    }
    
    // GET: api/event/search
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable>> GetFilteredEvents([FromQuery] string sport, [FromQuery] string location, [FromQuery] string gender)
    {
        var sportEvents = await _context.Events
            .Include(sportEvent => sportEvent.IdSportNavigation)
            .Include(sportEvent => sportEvent.IdLevelNavigation)
            .Include(sportEvent => sportEvent.IdUserNavigation)
            .Include(sportEvent => sportEvent.IdEventGenderNavigation)
            .Where(sportEvent => DateTime.Compare(DateTime.Now, sportEvent.Startdate) < 0 && sportEvent.IdSportNavigation.Name == sport && sportEvent.Location == location && sportEvent.IdEventGenderNavigation.Name == gender)
            .ToListAsync();
        
        return sportEvents;
    }
    
    // GET: api/event/:id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetEvent(int id)
    {
        var sportEvent = await _context.Events
            .Include(sportEvent => sportEvent.IdSportNavigation)
            .Include(sportEvent => sportEvent.IdLevelNavigation)
            .Include(sportEvent => sportEvent.IdUserNavigation)
            .FirstOrDefaultAsync(e => e.Id == id);
        if (sportEvent == null)
        {
            return NotFound();
        }
      
        return Ok(sportEvent);
    }

    // POST: api/event
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event sportEvent)
    {
      _context.Events.Add(sportEvent);
      await _context.SaveChangesAsync();
    
      return CreatedAtAction("GetEvent", new { id = sportEvent.Id }, sportEvent);
    }

    // DELETE: api/event/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var sportEvent = await _context.Events.FindAsync(id);
        if (sportEvent == null)
        {
            return NotFound();
        }

        _context.Events.Remove(sportEvent);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    // DELETE: api/event/5/1
    [HttpPost("{eventId}/{userId}")]
    public async Task<IActionResult> RegisterUserToEvent(int eventId, int userId)
    {
        var sportEvent = await _context.Events
            .Include(e => e.Ids)
            .FirstOrDefaultAsync(e => e.Id == eventId);
        if (sportEvent == null)
        {
            return NotFound("Event not found");
        }
        
        var user = await _context.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (sportEvent.Ids.Contains(user))
        {
            return Unauthorized("Already registered");
        }
        
        sportEvent.Ids.Add(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventExists(int id)
    {
        return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}