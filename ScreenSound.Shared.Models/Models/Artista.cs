using ScreenSound.Shared.Models.Models;

namespace ScreenSound.Modelos; 

public class Artista 
{
    public string Nome { get; set; }
    public string FotoPerfil { get; set; }
    public string Bio { get; set; }
    public int Id { get; set; }
    public virtual ICollection<Musica> Musicas { get; set; } = new List<Musica>();
    public virtual ICollection<AvaliacaoArtista> Avaliacoes { get; set; } = new List<AvaliacaoArtista>();

    public Artista(string nome, string bio)
    {
        Nome = nome;
        Bio = bio;
        FotoPerfil = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    }

    public void AdicionarMusica(Musica musica)
    {
        Musicas.Add(musica);
    }

    public void AdicionarNota(int pessoaId, int nota)
    {
        nota = Math.Clamp(nota, 1, 5); // Garante que a nota esteja entre 1 e 5
        Avaliacoes.Add(new AvaliacaoArtista() { ArtistaId = this.Id, PessoaId = pessoaId, Nota = nota });
    }

    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Nome}");
        foreach (var musica in Musicas)
        {
            Console.WriteLine($"Música: {musica.Nome} - Ano de lançamento: {musica.AnoLancamento}");
        }
    }

    public void ExibirBio()
    {
        Console.WriteLine($"Biografia  do artista {Nome}");

        Console.WriteLine($"=> {Bio}");
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Nome}
            Foto de Perfil: {FotoPerfil}
            Bio: {Bio}";
    }
}


/*
 * Classe Math e seus métodos, incluindo Min(), Max() e Clamp(); https://learn.microsoft.com/en-us/dotnet/api/system.math?view=net-9.0  
 * Operadores condicionais ternários. https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator
 */