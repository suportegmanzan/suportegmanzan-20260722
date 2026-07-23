using System.ComponentModel.DataAnnotations;

namespace GestaoColaboradores.Api.DTOs;

public class CriarUnidadeDTO
{
    [Required(ErrorMessage = "O código da unidade é obrigatório.")]
    [MaxLength(50)]
    public string CodigoUnidade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(200)]
    public string Nome { get; set; } = string.Empty;
}


public class AtualizarUnidadeDTO
{
    [MaxLength(200)]
    public string? Nome { get; set; }

    public bool? Ativa { get; set; }
}

public class UnidadeResumoDTO
{
    public int Id { get; set; }
    public string CodigoUnidade { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public bool Ativa { get; set; }
    public List<ColaboradorResumoDTO> Colaboradores { get; set; } = new();
}
