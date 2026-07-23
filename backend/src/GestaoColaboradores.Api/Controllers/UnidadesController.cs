using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradores.Api.Controllers;

public class UnidadesController : ApiControllerBase
{
    private readonly IUnidadeService _service;

    public UnidadesController(IUnidadeService service)
    {
        _service = service;
    }

    [HttpPost]
    public Task<ActionResult<UnidadeResumoDTO>> Criar(CriarUnidadeDTO dto)
        => ExecutarAsync(() => _service.CriarAsync(dto));

 
    [HttpPut("{id:int}")]
    public Task<ActionResult<UnidadeResumoDTO>> Atualizar(int id, AtualizarUnidadeDTO dto)
        => ExecutarAsync(() => _service.AtualizarAsync(id, dto));

    
    [HttpGet]
    public Task<ActionResult<List<UnidadeResumoDTO>>> Listar()
        => ExecutarAsync(() => _service.ListarAsync());
}
