using BlazorApp5.SQLServer.Data;
using BlazorApp5.SQLServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


[ApiController]
[Route("api/[controller]")]
public class LineitemsController : ControllerBase
{
    private readonly AppDbContext _context;

    private readonly ILogger<LineitemsController> _logger;

    public LineitemsController(AppDbContext context, ILogger<LineitemsController> logger)
    {
        _context = context;
        _logger = logger;
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

        // 获取 EntityEntry 对象
        var entry = _context.Entry(lineItem);

        // 记录 lineItem 对象的属性值
        _logger.LogInformation("开始更新 LineItem, ID: {LineItemId}", lineItem.Id);
        _logger.LogInformation("Customer: {Customer}, csfileName: {CsfileName}, lineName: {LineName}, partClass: {PartClass}",
            lineItem.Customer, lineItem.csfileName, lineItem.lineName, lineItem.partClass);

        // 记录 EntityState
        _logger.LogInformation("EntityState: {EntityState}", entry.State);

        // 设置 EntityState 为 Modified
        _context.Entry(lineItem).State = EntityState.Modified;

        try
        {
            // 保存更改
            await _context.SaveChangesAsync();
            _logger.LogInformation("LineItem 更新成功, ID: {LineItemId}", lineItem.Id);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!LineItemExists(id))
            {
                _logger.LogWarning("LineItem 未找到, ID: {LineItemId}", id);
                return NotFound();
            }
            else
            {
                _logger.LogError(ex, "更新 LineItem 时发生并发冲突, ID: {LineItemId}", id);
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新 LineItem 时发生异常, ID: {LineItemId}", id);
            return StatusCode(500, "Internal Server Error"); // 返回 500 状态码
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
