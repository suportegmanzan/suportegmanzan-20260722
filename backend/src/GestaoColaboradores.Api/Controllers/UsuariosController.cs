using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Models;
using GestaoColaboradores.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradores.Api.Controllers;

public class UsuariosController : ApiControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

   
    [HttpPost]
    [AllowAnonymous]
    public Task<ActionResult<UsuarioResumoDTO>> Criar(CriarUsuarioDTO dto)
        => ExecutarAsync(() => _service.CriarAsync(dto));

    
    [HttpPut("{id:int}")]
    public Task<ActionResult<UsuarioResumoDTO>> Atualizar(int id, AtualizarUsuarioDTO dto)
        => ExecutarAsync(() => _service.AtualizarAsync(id, dto));

  
    [HttpGet]
    public Task<ActionResult<List<UsuarioResumoDTO>>> Listar([FromQuery] StatusUsuario? status)
        => ExecutarAsync(() => _service.ListarAsync(status));
}
