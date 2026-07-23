using GestaoColaboradores.Api.Data;
using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Helpers;
using GestaoColaboradores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradores.Api.Services;

public class ColaboradorService : IColaboradorService
{
    private readonly AppDbContext _db;

    public ColaboradorService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<ColaboradorResumoDTO> CriarAsync(CriarColaboradorDTO dto)
    {
        var unidade = await _db.Unidades.FindAsync(dto.UnidadeId)
            ?? throw new RegraDeNegocioException("Unidade informada não existe.", 404);

        if (!unidade.Ativa)
            throw new RegraDeNegocioException("Não é possível incluir um colaborador em uma unidade inativa.", 400);

        var usuario = await _db.Usuarios.FindAsync(dto.UsuarioId)
            ?? throw new RegraDeNegocioException("Usuário informado não existe.", 404);

        var usuarioJaVinculado = await _db.Colaboradores.AnyAsync(c => c.UsuarioId == dto.UsuarioId);
        if (usuarioJaVinculado)
            throw new RegraDeNegocioException("Esse usuário já está vinculado a outro colaborador.", 409);

        var colaborador = new Colaborador
        {
            Nome = dto.Nome,
            UnidadeId = dto.UnidadeId,
            UsuarioId = dto.UsuarioId
        };

        _db.Colaboradores.Add(colaborador);
        await _db.SaveChangesAsync();

        return await MapearAsync(colaborador.Id);
    }

    public async Task<ColaboradorResumoDTO> AtualizarAsync(int id, AtualizarColaboradorDTO dto)
    {
        var colaborador = await _db.Colaboradores.FindAsync(id)
            ?? throw new RegraDeNegocioException("Colaborador não encontrado.", 404);

        var unidade = await _db.Unidades.FindAsync(dto.UnidadeId)
            ?? throw new RegraDeNegocioException("Unidade informada não existe.", 404);

        if (!unidade.Ativa && colaborador.UnidadeId != dto.UnidadeId)
            throw new RegraDeNegocioException("Não é possível mover um colaborador para uma unidade inativa.", 400);

        colaborador.Nome = dto.Nome;
        colaborador.UnidadeId = dto.UnidadeId;
        colaborador.DataAtualizacao = DateTime.UtcNow;

        await _db.SaveChangesAsync();

        return await MapearAsync(colaborador.Id);
    }

    public async Task RemoverAsync(int id)
    {
        var colaborador = await _db.Colaboradores.FindAsync(id)
            ?? throw new RegraDeNegocioException("Colaborador não encontrado.", 404);

        _db.Colaboradores.Remove(colaborador);
        await _db.SaveChangesAsync();
    }

    public async Task<List<ColaboradorResumoDTO>> ListarAsync()
    {
        return await _db.Colaboradores
            .Include(c => c.Unidade)
            .Include(c => c.Usuario)
            .OrderBy(c => c.Nome)
            .Select(c => new ColaboradorResumoDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                UnidadeId = c.UnidadeId,
                UnidadeNome = c.Unidade!.Nome,
                UsuarioLogin = c.Usuario!.Login
            })
            .ToListAsync();
    }

    private async Task<ColaboradorResumoDTO> MapearAsync(int id)
    {
        return await _db.Colaboradores
            .Include(c => c.Unidade)
            .Include(c => c.Usuario)
            .Where(c => c.Id == id)
            .Select(c => new ColaboradorResumoDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                UnidadeId = c.UnidadeId,
                UnidadeNome = c.Unidade!.Nome,
                UsuarioLogin = c.Usuario!.Login
            })
            .FirstAsync();
    }
}
