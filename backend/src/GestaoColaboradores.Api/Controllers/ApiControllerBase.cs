using GestaoColaboradores.Api.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradores.Api.Controllers;


[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected async Task<ActionResult<T>> ExecutarAsync<T>(Func<Task<T>> acao)
    {
        try
        {
            var resultado = await acao();
            return Ok(resultado);
        }
        catch (RegraDeNegocioException ex)
        {
            return StatusCode(ex.StatusCode, new { mensagem = ex.Message });
        }
    }

    protected async Task<IActionResult> ExecutarAsync(Func<Task> acao)
    {
        try
        {
            await acao();
            return NoContent();
        }
        catch (RegraDeNegocioException ex)
        {
            return StatusCode(ex.StatusCode, new { mensagem = ex.Message });
        }
    }
}
