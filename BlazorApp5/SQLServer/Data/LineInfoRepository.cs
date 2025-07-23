// Data/LineInfoRepository.cs
using BlazorApp5.SQLServer.Models;
using Microsoft.EntityFrameworkCore;


namespace BlazorApp5.SQLServer.Data
{
    public class LineInfoRepository
    {
        private readonly AppDbContext _context;

        public LineInfoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LineItem>> GetAllAsync()
        {
            return await _context.LineItems.AsNoTracking().ToListAsync();
        }

        public async Task<LineItem?> GetByIdAsync(int id)
        {
            return await _context.LineItems.FindAsync(id);
        }

        public async Task AddAsync(LineItem lineInfo)
        {
            _context.LineItems.Add(lineInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LineItem lineInfo)
        {
            _context.Entry(lineInfo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var lineInfo = await _context.LineItems.FindAsync(id);
            if (lineInfo != null)
            {
                _context.LineItems.Remove(lineInfo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.LineItems.AnyAsync(e => e.Id == id);
        }
    }
}
