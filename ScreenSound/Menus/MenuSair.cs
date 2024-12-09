using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuSair : Menu
{
    public override void Executar(DAL<Artista> artistaDal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
