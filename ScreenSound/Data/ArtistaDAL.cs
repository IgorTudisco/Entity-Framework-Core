using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ArtistaDAL : DAL<Artista>
{
    public ArtistaDAL(ScreenSoundContext context) : base(context) { }

    public Artista? FindByName(string name)
    {
        var artista = _context.Artistas.FirstOrDefault(findArtista => findArtista.Nome == name);
        return artista;
    }

}
