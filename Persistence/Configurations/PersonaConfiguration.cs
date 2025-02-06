using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{ 
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.Property(t => t.Nombres).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Apellidos).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Email).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Password).HasMaxLength(100).IsRequired();
        builder.Property(t => t.Telefono).HasMaxLength(100).IsRequired();
        builder.Property(t => t.FechaNacimiento).IsRequired();
        builder.Property(t => t.TypeId).IsRequired();
        builder.HasOne(t => t.Type).WithMany().HasForeignKey(t => t.TypeId);
        builder.Property(t => t.Metadata).IsRequired();
    }
}