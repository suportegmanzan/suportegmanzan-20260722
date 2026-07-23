using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestaoColaboradores.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace GestaoColaboradores.Api.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public (string token, DateTime expiraEm) GerarToken(Usuario usuario)
    {
        var chave = _config["Jwt:Chave"]!;
        var emissor = _config["Jwt:Emissor"];
        var audiencia = _config["Jwt:Audiencia"];
        var minutos = int.Parse(_config["Jwt:ExpiracaoMinutos"] ?? "120");

        var expiraEm = DateTime.UtcNow.AddMinutes(minutos);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
            new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new(ClaimTypes.Name, usuario.Login),
            new(ClaimTypes.Role, "Usuario")
        };

        var credenciais = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chave)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: emissor,
            audience: audiencia,
            claims: claims,
            expires: expiraEm,
            signingCredentials: credenciais);

        return (new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
    }
}
