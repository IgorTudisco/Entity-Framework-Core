using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ArtistaDAL
{
    
    private readonly ScreenSoundContext _context;

    public ArtistaDAL(ScreenSoundContext context)
    {
        _context = context;
    }

    public IEnumerable<Artista> ListarArtistas()
    {
        return _context.Artistas.ToList();
    }

    public void AdicionarArtista(Artista artista)
    {
        _context.Add(artista);
        _context.SaveChanges();
    }

    public void AtulaizaArtista(Artista artista)
    {
        _context.Update(artista);
        _context.SaveChanges();
    }


    public void ExcluirArtista(Artista artista)
    {
        _context.Remove(artista);
        _context.SaveChanges();
    }

    public Artista? FindByName(string name)
    {
        // Tem esse outro jeito de fazer.
        //return _context.Artistas.FirstOrDefault(a => a.Nome.Equals(name));

        var artista = _context.Artistas.FirstOrDefault(findArtista => findArtista.Nome == name);
        return artista;
    }

}
