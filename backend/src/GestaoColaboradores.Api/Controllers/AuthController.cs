using GestaoColaboradores.Api.DTOs;
using GestaoColaboradores.Api.Models;
using GestaoColaboradores.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestaoColaboradores.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;
    private readonly ITokenService _tokenService;

    public AuthController(IUsuarioService usuarioService, ITokenService tokenService)
    {
        _usuarioService = usuarioService;
        _tokenService = tokenService;
    }

 
    [HttpPost("login")]
    public async Task<ActionResult<LoginRespostaDTO>> Login(LoginDTO dto)
    {
        var usuario = await _usuarioService.BuscarPorLoginAsync(dto.Login);

        if (usuario is null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
            return Unauthorized(new { mensagem = "Login ou senha inválidos." });

        if (usuario.Status != StatusUsuario.Ativo)
            return Unauthorized(new { mensagem = "Usuário inativo. Procure o administrador do sistema." });

        var (token, expiraEm) = _tokenService.GerarToken(usuario);

        return Ok(new LoginRespostaDTO
        {
            Token = token,
            ExpiraEm = expiraEm,
            Login = usuario.Login
        });
    }
}
