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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany(us => us.Categories)
            .HasForeignKey(c => c.UserId);

        modelBuilder.Entity<Url>().ToTable("Url");
        modelBuilder.Entity<Url>()
            .HasOne(u => u.Category)
            .WithMany(c => c.Urls)
            .HasForeignKey(u => u.CategoryId);
        modelBuilder.Entity<Url>()
            .HasOne(u => u.User)
            .WithMany(us => us.Urls)
            .HasForeignKey(u => u.UserId);

    }
}
