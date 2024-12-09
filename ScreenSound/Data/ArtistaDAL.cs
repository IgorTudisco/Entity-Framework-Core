using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ArtistaDAL : DAL<Artista>
{
    public ArtistaDAL(ScreenSoundContext context) : base(context) { }

}
