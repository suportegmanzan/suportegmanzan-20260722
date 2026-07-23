namespace GestaoColaboradores.Api.Models;


public class Colaborador : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;

    public int UnidadeId { get; set; }
    public Unidade? Unidade { get; set; }

    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }
}
