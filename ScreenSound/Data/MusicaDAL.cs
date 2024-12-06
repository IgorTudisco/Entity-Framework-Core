using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data;

internal class MusicaDAL
{

    private readonly ScreenSoundContext _context;

    public MusicaDAL(ScreenSoundContext context)
    {
        _context = context;
    }

    public IEnumerable<Musica> ListaMusica()
    {
        return _context.Musicas.ToList();
    }

    public void AdicionaMusica(Musica musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }

    public void AtualizaMusica(Musica musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();
    }

    public void ExcluirMusica(Musica musica)
    {
        _context.Remove(musica);
        _context.SaveChanges();
    }

    public Musica? FindByNameMusica(string name)
    {
        return _context.Musicas.FirstOrDefault(a => a.Nome.Equals(name));
    }

}
