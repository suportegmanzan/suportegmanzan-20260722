using GestaoColaboradores.Api.Data;
using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Helpers;
using GestaoColaboradores.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoColaboradores.Api.Services;

public class UsuarioService : IUsuarioService
{
    private readonly AppDbContext _db;

    public UsuarioService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<UsuarioResumoDTO> CriarAsync(CriarUsuarioDTO dto)
    {
        var loginJaExiste = await _db.Usuarios.AnyAsync(u => u.Login == dto.Login);
        if (loginJaExiste)
            throw new RegraDeNegocioException("Já existe um usuário cadastrado com esse login.", 409);

        var usuario = new Usuario
        {
            Login = dto.Login,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            Status = dto.Status
        };

        _db.Usuarios.Add(usuario);
        await _db.SaveChangesAsync();

        return Mapear(usuario);
    }

    public async Task<UsuarioResumoDTO> AtualizarAsync(int id, AtualizarUsuarioDTO dto)
    {
        var usuario = await _db.Usuarios.FindAsync(id)
            ?? throw new RegraDeNegocioException("Usuário não encontrado.", 404);

       
        if (!string.IsNullOrWhiteSpace(dto.Senha))
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        if (dto.Status.HasValue)
            usuario.Status = dto.Status.Value;

        usuario.DataAtualizacao = DateTime.UtcNow;
        await _db.SaveChangesAsync();

        return Mapear(usuario);
    }

    public async Task<List<UsuarioResumoDTO>> ListarAsync(StatusUsuario? status)
    {
        var query = _db.Usuarios.AsQueryable();

        if (status.HasValue)
            query = query.Where(u => u.Status == status.Value);

        return await query
            .OrderBy(u => u.Login)
            .Select(u => new UsuarioResumoDTO { Id = u.Id, Login = u.Login, Status = u.Status })
            .ToListAsync();
    }

    public Task<Usuario?> BuscarPorLoginAsync(string login)
    {
        return _db.Usuarios.FirstOrDefaultAsync(u => u.Login == login);
    }

    private static UsuarioResumoDTO Mapear(Usuario usuario) => new()
    {
        Id = usuario.Id,
        Login = usuario.Login,
        Status = usuario.Status
    };
}
