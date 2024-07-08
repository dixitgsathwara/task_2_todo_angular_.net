using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UserContactController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserContactController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateContact([FromBody] Contact contact)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        if (userId == null)
        {
            return Unauthorized("Invalid token");
        }

        contact.userid = userId;
        _context.contacts.Add(contact);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetContact), new { id = contact.id }, contact);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetContact(int id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        if (userId == null)
        {
            return Unauthorized("Invalid token");
        }

        var contact = await _context.contacts.FirstOrDefaultAsync(c => c.id == id && c.userid == userId);
        if (contact == null)
        {
            return NotFound();
        }

        return Ok(contact);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetContacts()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        if (userId == null)
        {
            return Unauthorized("Invalid token");
        }

        var contacts = await _context.contacts.Where(c => c.userid == userId).ToListAsync();
        return Ok(contacts);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact updatedContact)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        if (userId == null)
        {
            return Unauthorized("Invalid token");
        }

        var contact = await _context.contacts.FirstOrDefaultAsync(c => c.id == id && c.userid == userId);
        if (contact == null)
        {
            return NotFound();
        }

        contact.name = updatedContact.name;
        contact.email = updatedContact.email;
        contact.phone = updatedContact.phone;

        _context.contacts.Update(contact);
        await _context.SaveChangesAsync();

        return NoContent();
    }


    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteContact(int id)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
        if (userId == null)
        {
            return Unauthorized("Invalid token");
        }

        var contact = await _context.contacts.FirstOrDefaultAsync(c => c.id == id && c.userid == userId);
        if (contact == null)
        {
            return NotFound();
        }

        _context.contacts.Remove(contact);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
