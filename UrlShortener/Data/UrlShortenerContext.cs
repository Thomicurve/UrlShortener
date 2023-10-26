using Microsoft.EntityFrameworkCore;

namespace UrlShortener;

public class UrlShortenerContext : DbContext
{
    public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options)
        : base(options)
    {
    }

    public DbSet<Url> Urls { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Url>().ToTable("Url");
        modelBuilder.Entity<Url>()
            .HasOne(u => u.Category)
            .WithMany(c => c.Urls)
            .HasForeignKey(u => u.CategoryId);

        
    }
}
