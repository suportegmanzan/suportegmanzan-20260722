using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Models;

namespace GestaoColaboradores.Api.Services;

public interface IUsuarioService
{
    Task<UsuarioResumoDTO> CriarAsync(CriarUsuarioDTO dto);
    Task<UsuarioResumoDTO> AtualizarAsync(int id, AtualizarUsuarioDTO dto);
    Task<List<UsuarioResumoDTO>> ListarAsync(StatusUsuario? status);
    Task<Usuario?> BuscarPorLoginAsync(string login);
}
