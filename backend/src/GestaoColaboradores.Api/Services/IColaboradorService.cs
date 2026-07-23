using GestaoColaboradores.Api.DTOs;

namespace GestaoColaboradores.Api.Services;

public interface IColaboradorService
{
    Task<ColaboradorResumoDTO> CriarAsync(CriarColaboradorDTO dto);
    Task<ColaboradorResumoDTO> AtualizarAsync(int id, AtualizarColaboradorDTO dto);
    Task RemoverAsync(int id);
    Task<List<ColaboradorResumoDTO>> ListarAsync();
}
