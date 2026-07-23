using System;
using GestaoColaboradores.Api.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GestaoColaboradores.Api.Migrations;

[DbContext(typeof(AppDbContext))]
[Migration("20260718030000_InitialCreate")]
public class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "unidades",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CodigoUnidade = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Ativa = table.Column<bool>(type: "boolean", nullable: false),
                DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_unidades", x => x.Id));

        migrationBuilder.CreateTable(
            name: "usuarios",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Login = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                SenhaHash = table.Column<string>(type: "text", nullable: false),
                Status = table.Column<int>(type: "integer", nullable: false),
                DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_usuarios", x => x.Id));

        migrationBuilder.CreateTable(
            name: "colaboradores",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                UnidadeId = table.Column<int>(type: "integer", nullable: false),
                UsuarioId = table.Column<int>(type: "integer", nullable: false),
                DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_colaboradores", x => x.Id);
                table.ForeignKey(
                    name: "FK_colaboradores_unidades_UnidadeId",
                    column: x => x.UnidadeId,
                    principalTable: "unidades",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_colaboradores_usuarios_UsuarioId",
                    column: x => x.UsuarioId,
                    principalTable: "usuarios",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_colaboradores_UnidadeId",
            table: "colaboradores",
            column: "UnidadeId");

        migrationBuilder.CreateIndex(
            name: "IX_colaboradores_UsuarioId",
            table: "colaboradores",
            column: "UsuarioId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_unidades_CodigoUnidade",
            table: "unidades",
            column: "CodigoUnidade",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_usuarios_Login",
            table: "usuarios",
            column: "Login",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "colaboradores");
        migrationBuilder.DropTable(name: "unidades");
        migrationBuilder.DropTable(name: "usuarios");
    }
}
