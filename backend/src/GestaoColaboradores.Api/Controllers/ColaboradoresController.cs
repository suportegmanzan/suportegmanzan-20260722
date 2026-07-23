using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradores.Api.Controllers;

public class ColaboradoresController : ApiControllerBase
{
    private readonly IColaboradorService _service;

    public ColaboradoresController(IColaboradorService service)
    {
        _service = service;
    }

    [HttpPost]
    public Task<ActionResult<ColaboradorResumoDTO>> Criar(CriarColaboradorDTO dto)
        => ExecutarAsync(() => _service.CriarAsync(dto));

    [HttpPut("{id:int}")]
    public Task<ActionResult<ColaboradorResumoDTO>> Atualizar(int id, AtualizarColaboradorDTO dto)
        => ExecutarAsync(() => _service.AtualizarAsync(id, dto));

    [HttpDelete("{id:int}")]
    public Task<IActionResult> Remover(int id)
        => ExecutarAsync(() => _service.RemoverAsync(id));

    [HttpGet]
    public Task<ActionResult<List<ColaboradorResumoDTO>>> Listar()
        => ExecutarAsync(() => _service.ListarAsync());
}
