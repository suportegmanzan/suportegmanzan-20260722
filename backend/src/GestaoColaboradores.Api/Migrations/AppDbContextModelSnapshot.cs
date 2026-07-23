using System;
using GestaoColaboradores.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestaoColaboradores.Api.Migrations;

[DbContext(typeof(AppDbContext))]
public class AppDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.8")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("GestaoColaboradores.Api.Models.Colaborador", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("integer")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
            entity.Property<DateTime?>("DataAtualizacao").HasColumnType("timestamp with time zone");
            entity.Property<DateTime>("DataCriacao").HasColumnType("timestamp with time zone");
            entity.Property<string>("Nome").IsRequired().HasMaxLength(200).HasColumnType("character varying(200)");
            entity.Property<int>("UnidadeId").HasColumnType("integer");
            entity.Property<int>("UsuarioId").HasColumnType("integer");
            entity.HasKey("Id");
            entity.HasIndex("UnidadeId");
            entity.HasIndex("UsuarioId").IsUnique();
            entity.ToTable("colaboradores");
        });

        modelBuilder.Entity("GestaoColaboradores.Api.Models.Unidade", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("integer")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
            entity.Property<bool>("Ativa").HasColumnType("boolean");
            entity.Property<string>("CodigoUnidade").IsRequired().HasMaxLength(50).HasColumnType("character varying(50)");
            entity.Property<DateTime?>("DataAtualizacao").HasColumnType("timestamp with time zone");
            entity.Property<DateTime>("DataCriacao").HasColumnType("timestamp with time zone");
            entity.Property<string>("Nome").IsRequired().HasMaxLength(200).HasColumnType("character varying(200)");
            entity.HasKey("Id");
            entity.HasIndex("CodigoUnidade").IsUnique();
            entity.ToTable("unidades");
        });

        modelBuilder.Entity("GestaoColaboradores.Api.Models.Usuario", entity =>
        {
            entity.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("integer")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
            entity.Property<DateTime?>("DataAtualizacao").HasColumnType("timestamp with time zone");
            entity.Property<DateTime>("DataCriacao").HasColumnType("timestamp with time zone");
            entity.Property<string>("Login").IsRequired().HasMaxLength(100).HasColumnType("character varying(100)");
            entity.Property<string>("SenhaHash").IsRequired().HasColumnType("text");
            entity.Property<int>("Status").HasColumnType("integer");
            entity.HasKey("Id");
            entity.HasIndex("Login").IsUnique();
            entity.ToTable("usuarios");
        });

        modelBuilder.Entity("GestaoColaboradores.Api.Models.Colaborador", entity =>
        {
            entity.HasOne("GestaoColaboradores.Api.Models.Unidade", "Unidade")
                .WithMany("Colaboradores").HasForeignKey("UnidadeId").OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.HasOne("GestaoColaboradores.Api.Models.Usuario", "Usuario")
                .WithOne("Colaborador").HasForeignKey("GestaoColaboradores.Api.Models.Colaborador", "UsuarioId")
                .OnDelete(DeleteBehavior.Restrict).IsRequired();
            entity.Navigation("Unidade");
            entity.Navigation("Usuario");
        });

        modelBuilder.Entity("GestaoColaboradores.Api.Models.Unidade", entity => entity.Navigation("Colaboradores"));
        modelBuilder.Entity("GestaoColaboradores.Api.Models.Usuario", entity => entity.Navigation("Colaborador"));
    }
}
