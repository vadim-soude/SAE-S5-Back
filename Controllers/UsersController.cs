using Microsoft.AspNetCore.Mvc;
using WorkshopAPI.Models;

namespace WorkshopAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly WorkshopContext _context;

    public UsersController(WorkshopContext context)
    {
        _context = context;
    }

    // POST: api/user/signup
    [HttpPost("signup")]
    public async Task<ActionResult> PostUser(User user)
    {
        if (CheckUserMail(user.Email))
        {
            return Unauthorized("Email already exists");
        }
        
        if (CheckUserUsername(user.Username))
        {
            return Unauthorized("Username already exists");
        }
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
    // GET: api/user/signin
    [HttpPost("signin")]
    public async Task<ActionResult<User>> AuthUser(Auth auth)
    {
        if (auth == null)
        {
            return BadRequest("Invalid request");
        }
        
        var user = FindUserByEmail(auth.Email);
        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }

        var result = FindUser(auth.Email, auth.Password);
        if (result == null)
        {
            return Unauthorized("Invalid email or password");
        }

        return result;
    }
    
    private bool CheckUserMail(string email)
    {
        return (_context.Users?.Any(e => e.Email == email)).GetValueOrDefault();
    }
    
    private bool CheckUserUsername(string username)
    {
        return (_context.Users?.Any(e => e.Username == username)).GetValueOrDefault();
    }

    private User FindUserByEmail(string email)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email);
    }

    private User FindUser(string email, string password)
    {
        return _context.Users.FirstOrDefault(user => user.Email == email && user.Password == password);
    }
}