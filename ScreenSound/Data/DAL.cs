
using ScreenSound.Modelos;

namespace ScreenSound.Data;

internal abstract class DAL<T> where T : class // Indicando que a ref de T será uma classe.
{

    protected readonly ScreenSoundContext _context;

    protected DAL(ScreenSoundContext context)
    {
        _context = context;
    }

    public IEnumerable<T> Listar()
    {
        return _context.Set<T>().ToList();
    }

    public void Adicionar(T objeto)
    {
        _context.Set<T>().Add(objeto);
        _context.SaveChanges();
    }

    public void Atualizar(T objeto)
    {
        _context.Set<T>().Update(objeto);
        _context.SaveChanges();
    }

    public void Excluir(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        _context.SaveChanges();
    }

    public T? FindByName(Func<T, bool> condition)
    {

        var artista = _context.Set<T>().FirstOrDefault(condition);
        return artista;

    }

}
