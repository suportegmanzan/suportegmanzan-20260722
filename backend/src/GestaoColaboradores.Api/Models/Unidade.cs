namespace GestaoColaboradores.Api.Models;


public class Unidade : EntidadeBase
{
    public string CodigoUnidade { get; set; } = string.Empty;

    public string Nome { get; set; } = string.Empty;

    public bool Ativa { get; set; } = true;

    public ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();
}
