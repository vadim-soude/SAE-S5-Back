using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkshopAPI.Models;

namespace WorkshopAPI.Controllers;

[Route("api/sport")]
[ApiController]
public class SportsController : ControllerBase
{
    private readonly WorkshopContext _context;

    public SportsController(WorkshopContext context)
    {
        _context = context;
    }

    // GET: api/sport
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sport>>> GetSports()
    {
      var sports = await _context.Sports
          .Include(sport => sport.IdSportCategoryNavigation)
          .ToListAsync();
      
      return sports;
    }
}