using System.ComponentModel.DataAnnotations;
using GestaoColaboradores.Api.Models;

namespace GestaoColaboradores.Api.DTOs;

public class CriarUsuarioDTO
{
    [Required(ErrorMessage = "O login é obrigatório.")]
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres.")]
    public string Senha { get; set; } = string.Empty;

    public StatusUsuario Status { get; set; } = StatusUsuario.Ativo;
}


public class AtualizarUsuarioDTO
{
    [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres.")]
    public string? Senha { get; set; }

    public StatusUsuario? Status { get; set; }
}

public class UsuarioResumoDTO
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public StatusUsuario Status { get; set; }
}
