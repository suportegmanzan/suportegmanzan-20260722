namespace GestaoColaboradores.Api.Models;


public class Usuario : EntidadeBase
{
    public string Login { get; set; } = string.Empty;

    public string SenhaHash { get; set; } = string.Empty;

    public StatusUsuario Status { get; set; } = StatusUsuario.Ativo;

    public Colaborador? Colaborador { get; set; }
}
