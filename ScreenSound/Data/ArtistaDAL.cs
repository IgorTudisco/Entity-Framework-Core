using Microsoft.Data.SqlClient;
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal class ArtistaDAL : DAL<Artista>
{
    public ArtistaDAL(ScreenSoundContext context) : base(context) { }

    public override IEnumerable<Artista> Listar()
    {
        return _context.Artistas.ToList();
    }

    public override void Adicionar(Artista artista)
    {
        _context.Add(artista);
        _context.SaveChanges();
    }

    public override void Atualizar(Artista artista)
    {
        _context.Update(artista);
        _context.SaveChanges();
    }


    public override void Excluir(Artista artista)
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
