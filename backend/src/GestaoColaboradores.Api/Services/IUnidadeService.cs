using GestaoColaboradores.Api.DTOs;

namespace GestaoColaboradores.Api.Services;

public interface IUnidadeService
{
    Task<UnidadeResumoDTO> CriarAsync(CriarUnidadeDTO dto);
    Task<UnidadeResumoDTO> AtualizarAsync(int id, AtualizarUnidadeDTO dto);
    Task<List<UnidadeResumoDTO>> ListarAsync();
}
