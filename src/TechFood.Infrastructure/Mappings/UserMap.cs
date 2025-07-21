using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechFood.Common.Entities;
using TechFood.Infrastructure.Data.ValueObjectMappings;
using TechFood.Infrastructure.DataValueObjectMappings;

namespace TechFood.Infrastructure.Data.Mappings;

public class UserMap : IEntityTypeConfiguration<UserDTO>
{
    public void Configure(EntityTypeBuilder<UserDTO> builder)
    {
        builder.ToTable("User");

        builder.OwnsOne(u => u.Name, name => name.MapName())
            .Navigation(u => u.Name)
            .IsRequired();

        builder.Property(u => u.Username)
            .IsRequired()
            .HasMaxLength(255);

        builder.OwnsOne(u => u.Email, email => email.MapEmail())
            .Navigation(u => u.Email);

        builder.Property(u => u.PasswordHash)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.Role)
            .IsRequired();
    }
}
