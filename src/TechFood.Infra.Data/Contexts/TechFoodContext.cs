using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechFood.Domain.Entities;
using TechFood.Domain.Enums;
using TechFood.Domain.Shared.Entities;

namespace TechFood.Infra.Data.Contexts;

public class TechFoodContext(
    DbContextOptions<TechFoodContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Customer> Customers { get; set; } = null!;

    public DbSet<Order> Orders { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<Payment> Payments { get; set; } = null!;

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

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(Entity).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .HasKey(nameof(Entity.Id));

                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(Entity.Id))
                    .IsRequired()
                    .ValueGeneratedNever();
            }
        }

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
        modelBuilder.Entity<Customer>()
            .HasData(
                new { Id = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8") }
            );

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Name)
           .HasData(
               new { CustomerId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), FullName = "John" }
           );

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Document)
          .HasData(
              new { CustomerId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), Type = DocumentType.CPF, Value = "4511554544" }
          );

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Email)
          .HasData(
              new { CustomerId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), Address = "john.dev@gmail.com" }
          );

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Phone)
          .HasData(
              new { CustomerId = new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), CountryCode = "55", DDD = "11", Number = "9415452222" }
          );

        modelBuilder.Entity<Category>()
            .HasData(
                new { Id = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), Name = "Lanche", ImageFileName = "lanche.png" },
                new { Id = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), Name = "Acompanhamento", ImageFileName = "acompanhamento.png" },
                new { Id = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), Name = "Bebida", ImageFileName = "bebida.png" },
                new { Id = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), Name = "Sobremesa", ImageFileName = "sobremesa.png" }
            );

        modelBuilder.Entity<Product>()
            .HasData(
                new { Id = new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), Name = "X-Burguer", Description = "Delicioso X-Burguer", Price = 19.99m, CategoryId = new Guid("eaa76b46-2e6b-42eb-8f5d-b213f85f25ea"), ImageFileName = "lanche-carnes.png", OutOfStock = false },
                new { Id = new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), Name = "Batata Frita", Description = "Crocante Batata Frita", Price = 9.99m, CategoryId = new Guid("c65e2cec-bd44-446d-8ed3-a7045cd4876a"), ImageFileName = "bebida-gelada.png", OutOfStock = false },
                new { Id = new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), Name = "Refrigerante", Description = "Gelado Refrigerante", Price = 4.99m, CategoryId = new Guid("c3a70938-9e88-437d-a801-c166d2716341"), ImageFileName = "bebida-gelada.png", OutOfStock = false },
                new { Id = new Guid("de797d9f-c473-4bed-a560-e7036ca10ab1"), Name = "Pudim", Description = "Doce Pudim", Price = 7.99m, CategoryId = new Guid("ec2fb26d-99a4-4eab-aa5c-7dd18d88a025"), ImageFileName = "bebida-gelada.png", OutOfStock = false }
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
