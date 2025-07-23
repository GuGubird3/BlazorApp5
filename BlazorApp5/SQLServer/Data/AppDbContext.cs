// Data/AppDbContext.cs
using BlazorApp5.SQLServer.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp5.SQLServer.Data
{


    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<LineItem> LineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 配置LineInfo实体
            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.HasKey(e => e.Id); // 添加主键
                entity.Property(e => e.Customer).HasMaxLength(100);
                entity.Property(e => e.lineName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.partClass).HasMaxLength(50);
                entity.Property(e => e.csfileName).HasMaxLength(100);
            });
        }
    }
}
