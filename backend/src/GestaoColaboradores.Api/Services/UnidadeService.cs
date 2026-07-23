using GestaoColaboradores.Api.Data;
using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Helpers;
using GestaoColaboradores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradores.Api.Services;

public class UnidadeService : IUnidadeService
{
    private readonly AppDbContext _db;

    public UnidadeService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UnidadeResumoDTO> CriarAsync(CriarUnidadeDTO dto)
    {
        var codigoJaExiste = await _db.Unidades.AnyAsync(u => u.CodigoUnidade == dto.CodigoUnidade);
        if (codigoJaExiste)
            throw new RegraDeNegocioException("Já existe uma unidade cadastrada com esse código.", 409);

        var unidade = new Unidade
        {
            CodigoUnidade = dto.CodigoUnidade,
            Nome = dto.Nome,
            Ativa = true
        };

        _db.Unidades.Add(unidade);
        await _db.SaveChangesAsync();

        return Mapear(unidade);
    }

    public async Task<UnidadeResumoDTO> AtualizarAsync(int id, AtualizarUnidadeDTO dto)
    {
        var unidade = await _db.Unidades
            .Include(u => u.Colaboradores)
            .ThenInclude(c => c.Usuario)
            .FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new RegraDeNegocioException("Unidade não encontrada.", 404);

        if (!string.IsNullOrWhiteSpace(dto.Nome))
            unidade.Nome = dto.Nome;

        if (dto.Ativa.HasValue)
            unidade.Ativa = dto.Ativa.Value;

        unidade.DataAtualizacao = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Mapear(unidade);
    }

    public async Task<List<UnidadeResumoDTO>> ListarAsync()
    {
        var unidades = await _db.Unidades
            .Include(u => u.Colaboradores)
            .ThenInclude(c => c.Usuario)
            .OrderBy(u => u.Nome)
            .ToListAsync();

        return unidades.Select(Mapear).ToList();
    }

    private static UnidadeResumoDTO Mapear(Unidade unidade) => new()
    {
        Id = unidade.Id,
        CodigoUnidade = unidade.CodigoUnidade,
        Nome = unidade.Nome,
        Ativa = unidade.Ativa,
        Colaboradores = unidade.Colaboradores.Select(c => new ColaboradorResumoDTO
        {
            Id = c.Id,
            Nome = c.Nome,
            UnidadeId = c.UnidadeId,
            UnidadeNome = unidade.Nome,
            UsuarioLogin = c.Usuario?.Login ?? string.Empty
        }).ToList()
    };
}
