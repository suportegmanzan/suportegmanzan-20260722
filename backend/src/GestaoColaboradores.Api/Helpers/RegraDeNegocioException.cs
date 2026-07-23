namespace GestaoColaboradores.Api.Helpers;

public class RegraDeNegocioException : Exception
{
    public int StatusCode { get; }

    public RegraDeNegocioException(string mensagem, int statusCode = 400) : base(mensagem)
    {
        StatusCode = statusCode;
    }
}
