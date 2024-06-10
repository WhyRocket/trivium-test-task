using Microsoft.EntityFrameworkCore;

namespace Dictionaries.Entities;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToTable("products");

        modelBuilder.Entity<Product>()
            .Property(p => p.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Product>()
            .Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(p => p.Code)
            .HasColumnName("code")
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Product>().HasData(
        [
            new Product
            {
                Id = 1,
                Name = "Гвозди",
                Code = "G-001"
            },
            new Product
            {
                Id = 2,
                Name = "Шурупы",
                Code = "SH-20"
            },
            new Product
            {
                Id = 3,
                Name = "Саморезы",
                Code = "SA-19"
            },
            new Product
            {
                Id = 4,
                Name = "Болты",
                Code = "B-100"
            }
        ]);
    }
}