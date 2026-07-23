using System.ComponentModel.DataAnnotations;

namespace GestaoColaboradores.Api.DTOs;

public class LoginDTO
{
    [Required]
    public string Login { get; set; } = string.Empty;

    [Required]
    public string Senha { get; set; } = string.Empty;
}

public class LoginRespostaDTO
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiraEm { get; set; }
    public string Login { get; set; } = string.Empty;
}
