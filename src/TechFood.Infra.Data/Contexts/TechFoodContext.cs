using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;

namespace TechFood.Infra.Data.Contexts;

public class TechFoodContext(
    DbContextOptions<TechFoodContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechFoodContext).Assembly);

        var properties = modelBuilder.Model
            .GetEntityTypes()
            .SelectMany(t => t.GetProperties());

        var stringProperties = properties.Where(p => p.ClrType == typeof(string));
        foreach (var property in stringProperties)
        {
            var maxLength = property.GetMaxLength() ?? 50;

            property.SetColumnType($"varchar({maxLength})");
        }

        var booleanProperties = properties
            .Where(p => p.ClrType == typeof(bool) ||
                        p.ClrType == typeof(bool?));

        foreach (var property in booleanProperties)
        {
            property.SetColumnType("bit");
            property.IsNullable = false;
        }

        var dateTimeProperties = properties.Where(p => p.ClrType == typeof(DateTime));

        foreach (var property in dateTimeProperties)
        {
            property.SetColumnType("datetime");
        }

        var enumProperties = properties.Where(p => p.ClrType == typeof(Enum));

        foreach (var property in enumProperties)
        {
            property.SetColumnType("smallint");
        }

        var amountProperties = properties
            .Where(p => p.ClrType == typeof(decimal) ||
                        p.ClrType == typeof(decimal?));

        foreach (var property in amountProperties)
        {
            property.SetColumnType("decimal(6, 2)");
        }

        SeedContext(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedContext(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasData(
                new { Id = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), Name = "Lanche", ImageFileName = "lanche.jpg" },
                new { Id = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), Name = "Acompanhamento", ImageFileName = "acompanhamento.png" },
                new { Id = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), Name = "Bebida", ImageFileName = "bebida.jpg" },
                new { Id = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), Name = "Sobremesa", ImageFileName = "sobremesa.jpg" }
            );

        //modelBuilder.Entity<PaymentType>()
        //    .HasData(
        //        new { Id = 1, Code = "MCMA", Description = "Mastercard" },
        //        new { Id = 2, Code = "VIS", Description = "Visa" },
        //        new { Id = 3, Code = "ELO", Description = "Elo" },
        //        new { Id = 4, Code = "DNR", Description = "Sodexo" },
        //        new { Id = 5, Code = "VR", Description = "Vale Refeição" },
        //        new { Id = 6, Code = "PIX", Description = "Pix" }
        //    );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        optionsBuilder.LogTo(Console.WriteLine);
#endif
    }
}
