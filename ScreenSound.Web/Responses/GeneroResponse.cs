namespace ScreenSound.Web.Response;

public record class GeneroResponse(int IdGenero, string? Nome, string? Descricao)
{
    public override string ToString()
    {
        return $"{this.Nome}";
    }
};
