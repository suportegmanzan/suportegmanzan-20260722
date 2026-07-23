using System.ComponentModel.DataAnnotations;

namespace GestaoColaboradores.Api.DTOs;

public class CriarColaboradorDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A unidade é obrigatória.")]
    public int UnidadeId { get; set; }

    [Required(ErrorMessage = "O usuário relacionado é obrigatório.")]
    public int UsuarioId { get; set; }
}

public class AtualizarColaboradorDTO
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A unidade é obrigatória.")]
    public int UnidadeId { get; set; }
}

public class ColaboradorResumoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int UnidadeId { get; set; }
    public string UnidadeNome { get; set; } = string.Empty;
    public string UsuarioLogin { get; set; } = string.Empty;
}
