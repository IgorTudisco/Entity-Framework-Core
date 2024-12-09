using Microsoft.EntityFrameworkCore;
using ScreenSound.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenSound.Data;

internal class MusicaDAL : DAL<Musica>
{

    private readonly ScreenSoundContext _context;

    public MusicaDAL(ScreenSoundContext context)
    {
        _context = context;
    }

    public override IEnumerable<Musica> Listar()
    {
        return _context.Musicas.ToList();
    }

    public override void Adicionar(Musica musica)
    {
        _context.Musicas.Add(musica);
        _context.SaveChanges();
    }

    public override void Atualizar(Musica musica)
    {
        _context.Musicas.Update(musica);
        _context.SaveChanges();
    }

    public override void Excluir(Musica musica)
    {
        _context.Remove(musica);
        _context.SaveChanges();
    }

    public Musica? FindByNameMusica(string name)
    {
        return _context.Musicas.FirstOrDefault(a => a.Nome.Equals(name));
    }

}
