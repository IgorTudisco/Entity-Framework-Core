
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

public class MenuMostrarBioArtista : Menu
{
    public override void Executar(DAL<Artista> artistaDal)
    {
        base.Executar(artistaDal);
        ExibirTituloDaOpcao("Exibir detalhes do artista");
        Console.Write("Digite o nome do artista que deseja conhecer melhor: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaRecuperado = artistaDal.FindBy(a => a.Nome.Equals(nomeDoArtista));
        if (artistaRecuperado is not null)
        {
            Console.WriteLine("\nBiografia :");
            artistaRecuperado.ExibirBio();
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }

}
