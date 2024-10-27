using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManager.Data;
using PasswordManager.Models;

namespace PasswordManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PasswordController : ControllerBase
{
    private readonly PasswordContext _context;

    public PasswordController(PasswordContext context)
    {
        _context = context;
    }
    
    // POST: api/password - Generate new password
    [HttpPost]
    public async Task<ActionResult<Password>> CreatePassword(Password password)
    {
        _context.Passwords.Add(password);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPassword), new
        {
            id = password.Id
        }, password);
    }
    
    // GET: api/password/{id} - Get an password by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Password>> GetPassword(int id)
    {
        var password = await _context.Passwords.FindAsync(id);

        if (password == null)
        {
            return NotFound();
        }

        return password;
    }
    
    // GET: api/password - Get all passwords
    [HttpGet]
    public async Task<ActionResult<List<Password>>> GetPasswords()
    {
        return await _context.Passwords.ToListAsync();
    }
    
    // PUT: api/password/{id} - Update a password
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePassword(int id, Password password)
    {
        if (id != password.Id)
        {
            return BadRequest();
        }

        _context.Entry(password).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PasswordExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    // DELETE: api/password/{id} - delete a password
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePassword(int id)
    {
        var password = await _context.Passwords.FindAsync(id);
        if (password == null)
        {
            return NotFound();
        }

        _context.Passwords.Remove(password);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PasswordExists(int id)
    {
        return _context.Passwords.Any(e => e.Id == id);
    }
}