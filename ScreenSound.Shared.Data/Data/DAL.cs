
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ScreenSound.Data;

public class DAL<T> where T : class
{

    private readonly ScreenSoundContext _context;
    private readonly DbSet<T> _dbSet;

    public DAL(ScreenSoundContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> QueryIncludes(params Expression<Func<T, object?>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        // Aplica os includes dinâmicos
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query.ToList();
    }

    public IEnumerable<T> Listar()
    {
        return _dbSet.ToList();
    }

    public IEnumerable<T> Listar(params Expression<Func<T, object?>>[] includes)
    {
        return QueryIncludes(includes);
    }

    public void Adicionar(T objeto)
    {
        _dbSet.Add(objeto);
        _context.SaveChanges();
    }

    public void Atualizar(T objeto)
    {
        _dbSet.Update(objeto);
        _context.SaveChanges();
    }

    public void Excluir(T objeto)
    {
        _context.Set<T>().Remove(objeto);
        _context.SaveChanges();
    }

    public T? FindBy(Func<T, bool> condition)
    {
        var artista = _dbSet.FirstOrDefault(condition);
        return artista;
    }

    public IEnumerable<T> FindBy(Func<T, bool> condicao, params Expression<Func<T, object?>>[] includes)
    {
        var list = QueryIncludes(includes);
        return list.Where(condicao).ToList();
    }
}


/*
 
 * No C#, trabalhamos com o Func para encapsular um método que geralmente tem um parâmetro e retorna um valor do tipo especificado pelo parâmetro TResult.
 
 * Ele é muito útil para situações como a que passamos no vídeo anterior, quando precisamos representar um método sem ter que passar especificamente valores
 * personalizados, deixando a correspondência para a assinatura do método.
 
 * Existem outras variações de utilização do Func, com mais parâmetros e condições, por exemplo, e para conhecer mais a fundo sobre eles e ver exemplos de
 * aplicação você pode consultar a documentação oficial (https://learn.microsoft.com/pt-br/dotnet/api/system.func-2?view=net-8.0).
 
 */