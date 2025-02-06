using System.Reflection;
using System.Text.Json;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interceptors;

namespace Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    static ApplicationDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Catalogo> Catalogos { get; set; }
    public DbSet<ContenidoCatalogo> ContenidoCatalogos { get; set; }
    public DbSet<FormField> FormFields { get; set; }
    public DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Catalogo>().ToTable("Catalogos");
        modelBuilder.Entity<ContenidoCatalogo>().ToTable("ContenidoCatalogos");
        modelBuilder.Entity<FormField>().ToTable("FormFields");
        modelBuilder.Entity<Persona>().ToTable("Personas");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Catalogo>().HasData(
            new Catalogo
            {
                Id = 1,
                Nombre = "Roles",
                Descripcion = "Catalogo que identifica los roles de la aplicacion",
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 12, 0, 0), DateTimeKind.Utc), // Fecha fija
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 12, 0, 0), DateTimeKind.Utc) // Fecha fija
            },
            new Catalogo
            {
                Id = 2,
                Nombre = "Rutas",
                Descripcion = "Catalogo que identifica las rutas de la aplicacion",
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 12, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 12, 0, 0), DateTimeKind.Utc)
            },
            new Catalogo
            {
                Id = 3,
                Nombre = "Formularios",
                Descripcion = "Catalogo que identifica los formularios de la aplicacion",
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 12, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2024, 1, 1, 12, 0, 0), DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<ContenidoCatalogo>().HasData(
            new ContenidoCatalogo
            {
                Id = 1,
                Nombre = "Login",
                Descripcion = "Formulario de login",
                Opcional = "",
                CatalogoId = 1,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new ContenidoCatalogo
            {
                Id = 2,
                Nombre = "Registro",
                Descripcion = "Formulario de registro",
                Opcional = "",
                CatalogoId = 1,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new ContenidoCatalogo
            {
                Id = 3,
                Nombre = "loginscreen",
                Descripcion = "Pantalla de login",
                Opcional = "",

                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new ContenidoCatalogo
            {
                Id = 4,
                Nombre = "register",
                Descripcion = "Pantalla de registar",
                Opcional = "",

                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new ContenidoCatalogo
            {
                Id = 5,
                Nombre = "Dashboard",
                Descripcion = "Pantalla de inicio",
                Opcional = "",

                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<FormField>()
            .Property(f => f.Validations)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                v => JsonSerializer.Deserialize<List<FormValidation>>(v,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            );

        modelBuilder.Entity<FormField>()
            .Property(f => f.Options)
            .HasConversion(
                v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                v => JsonSerializer.Deserialize<List<SelectFormOption>>(v,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            );
        modelBuilder.Entity<FormField>()
            .HasOne<FormField>()
            .WithMany()
            .HasForeignKey(f => f.CatalogoId)
            .IsRequired(false); // <-- Permite valores nulos

        modelBuilder.Entity<FormField>().HasData(
            new FormField
            {
                Id = 1,
                Name = "email",
                Type = "text",
                Placeholder = "Email/Usuario",
                Label = "Email/Usuario",
                Value = " ",

                FormularioId = 1,
                Order = 0,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new FormField
            {
                Id = 2,
                Name = "password",
                Type = "password",
                Placeholder = "Password",
                Label = "Password",
                Value = " ",

                FormularioId = 1,
                Order = 1,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new FormField
            {
                Id = 3,
                Name = "email",
                Type = "text",
                Placeholder = "Email/Usuario",
                Label = "Email/Usuario",
                Value = " ",

                FormularioId = 2,
                Order = 0,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new FormField
            {
                Id = 4,
                Name = "password",
                Type = "password",
                Placeholder = "Password",
                Label = "Password",
                Value = " ",

                FormularioId = 2,
                Order = 1,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new FormField
            {
                Id = 5,
                Name = "confirmPassword",
                Type = "password",
                Placeholder = "Confirmar Password",
                Label = "Confirmar Password",
                Value = " ",

                FormularioId = 2,
                Order = 2,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new FormField
            {
                Id = 6,
                Name = "nombre",
                Type = "text",
                Placeholder = "Nombre",
                Label = "Nombre",
                Value = " ",

                FormularioId = 2,
                Order = 3,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            },
            new FormField
            {
                Id = 7,
                Name = "apellido",
                Type = "text",
                Placeholder = "Apellido",
                Label = "Apellido",
                Value = " ",

                FormularioId = 2,
                Order = 4,
                Active = true,
                CreatedBy = "System",
                Created = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc),
                LastModifiedBy = "System",
                LastModified = DateTime.SpecifyKind(new DateTime(2025, 1, 29, 0, 0, 0), DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<Persona>()
            .Property(e => e.Metadata)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null), // Convierte a JSON
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v,
                    (JsonSerializerOptions?)null) // Convierte desde JSON
            );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
        optionsBuilder.EnableSensitiveDataLogging();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}