using Microsoft.EntityFrameworkCore;

namespace Dictionaries.Entities;

// Контекст базы данных (позволяет взаимодействовать с БД).
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    // Таблица продукции.
    public DbSet<Product> Products { get; set; }
    // Таблица заводов.
    public DbSet<Factory> Factories { get; set; }

    // Конфигурация сущностей.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Создание таблицы products в БД.
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

        // Инициализация таблицы products данными.
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

        // Создание таблицы factory в БД.
        modelBuilder.Entity<Factory>()
            .ToTable("factory");

        modelBuilder.Entity<Factory>()
            .Property(f => f.Id)
            .HasColumnName("id");

        modelBuilder.Entity<Factory>()
            .Property(f => f.Name)
            .HasColumnName("name");

        modelBuilder.Entity<Factory>()
            .Property(f => f.Region)
            .HasColumnName("region");

        modelBuilder.Entity<Factory>()
            .Property(f => f.Year)
            .HasColumnName("year");

        modelBuilder.Entity<Factory>()
            .HasKey(f => f.Id);

        // Инициализация тиблицы factory данными.
        modelBuilder.Entity<Factory>().HasData(
        [
            new Factory
            {
                Id = 1,
                Name = "Завод 1",
                Region = "Пермский край",
                Year = 1968
            },
            new Factory
            {
                Id = 2,
                Name = "Завод 2",
                Region = "Татарстан",
                Year = 2001
            },
            new Factory
            {
                Id = 3,
                Name = "Завод 3",
                Region = "Пермский край",
                Year = 1998
            }
        ]);
    }
}