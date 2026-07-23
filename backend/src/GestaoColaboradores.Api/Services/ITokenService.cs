using GestaoColaboradores.Api.Models;

namespace GestaoColaboradores.Api.Services;

public interface ITokenService
{
    (string token, DateTime expiraEm) GerarToken(Usuario usuario);
}
