using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using WorkshopAPI.Models;

namespace WorkshopAPI.Controllers;

[Route("api/membre")]
[ApiController]
public class MembreController : ControllerBase
{
    private readonly bdeContext _context;
    private const int PBKDF2IterCount = 1000; // default for Rfc2898DeriveBytes
    private const int PBKDF2SubkeyLength = 256/8; // 256 bits
    private const int SaltSize = 128/8; // 128 bit
    
    public MembreController(bdeContext context)
    {
        _context = context;
    }
    
    // POST: api/membre/signup
    [HttpPost("signup")]
    public async Task<ActionResult> PostUser(Membre membre)
    {
        if (membre.Password == null)
        {
            return Unauthorized("Password is null");
        }

        if ((_context.Membres?.Any(e => e.MailUpjv == membre.MailUpjv)).GetValueOrDefault())
        {
            return Unauthorized("Email already exists");
        }

        membre.Password = HashPassword(membre.Password);
        
        _context.Membres.Add(membre);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
    // Post: api/membre/login
    [HttpPost("login")]
    public async Task<ActionResult<Membre>> Login(Login login)
    {
        var user = _context.Membres.FirstOrDefault(membre => membre.MailUpjv == login.MailUpjv);
        if (user == null)
        {
            return Unauthorized("No Account find with this email");
        }

        bool passwordValid = VerifyHashedPassword(user.Password, login.Password);
        if (!passwordValid)
        {
            return Unauthorized("Invalid password");
        }

        return user;
    }
    
    public static string HashPassword(string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }

        // Produce a version 0 (see comment above) text hash.
        byte[] salt;
        byte[] subkey;
        using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
        {
            salt = deriveBytes.Salt;
            subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
        }

        var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
        return Convert.ToBase64String(outputBytes);
    }
    
    public static bool VerifyHashedPassword(string hashedPassword, string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException("password");
        }

        var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

        // Verify a version 0 (see comment above) text hash.

        if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
        {
            // Wrong length or version header.
            return false;
        }

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
        var storedSubkey = new byte[PBKDF2SubkeyLength];
        Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

        byte[] generatedSubkey;
        using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2IterCount))
        {
            generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
        }
        return ByteArraysEqual(storedSubkey, generatedSubkey);
    }
    
    [MethodImpl(MethodImplOptions.NoOptimization)]
    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (ReferenceEquals(a, b))
        {
            return true;
        }

        if (a == null || b == null || a.Length != b.Length)
        {
            return false;
        }

        var areSame = true;
        for (var i = 0; i < a.Length; i++)
        {
            areSame &= (a[i] == b[i]);
        }
        return areSame;
    }
}