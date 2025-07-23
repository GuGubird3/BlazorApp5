using BlazorApp5.SQLServer.Data;
using BlazorApp5.SQLServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class LineitemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public LineitemsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItems()
    {
        return await _context.LineItems.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LineItem?>> GetLineItem(int id)
    {
        var lineItem = await _context.LineItems.FindAsync(id);

        if (lineItem == null)
        {
            return NotFound();
        }

        return lineItem;
    }

    [HttpPost]
    public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
    {
        _context.LineItems.Add(lineItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetLineItem), new { id = lineItem.Id }, lineItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutLineItem(int id, LineItem lineItem)
    {
        if (id != lineItem.Id)
        {
            return BadRequest();
        }

        _context.Entry(lineItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!LineItemExists(id))
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLineItem(int id)
    {
        var lineItem = await _context.LineItems.FindAsync(id);
        if (lineItem == null)
        {
            return NotFound();
        }

        _context.LineItems.Remove(lineItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool LineItemExists(int id)
    {
        return _context.LineItems.Any(e => e.Id == id);
    }
}
