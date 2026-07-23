using GestaoColaboradores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradores.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Colaborador> Colaboradores => Set<Colaborador>();
    public DbSet<Unidade> Unidades => Set<Unidade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Login).IsRequired().HasMaxLength(100);
            entity.HasIndex(u => u.Login).IsUnique();
            entity.Property(u => u.SenhaHash).IsRequired();
            entity.Property(u => u.Status).HasConversion<int>();
        });

        modelBuilder.Entity<Unidade>(entity =>
        {
            entity.ToTable("unidades");
            entity.HasKey(u => u.Id);
            entity.Property(u => u.CodigoUnidade).IsRequired().HasMaxLength(50);
            entity.HasIndex(u => u.CodigoUnidade).IsUnique();
            entity.Property(u => u.Nome).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Colaborador>(entity =>
        {
            entity.ToTable("colaboradores");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nome).IsRequired().HasMaxLength(200);

            entity.HasOne(c => c.Unidade)
                  .WithMany(u => u.Colaboradores)
                  .HasForeignKey(c => c.UnidadeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Usuario)
                  .WithOne(u => u.Colaborador)
                  .HasForeignKey<Colaborador>(c => c.UsuarioId)
                  .OnDelete(DeleteBehavior.Restrict);

            
            entity.HasIndex(c => c.UsuarioId).IsUnique();
        });
    }
}
