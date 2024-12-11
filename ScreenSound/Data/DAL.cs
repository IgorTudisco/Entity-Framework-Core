
namespace ScreenSound.Data;

internal class DAL<T> where T : class
{

    protected readonly ScreenSoundContext _context;

    public DAL(ScreenSoundContext context)
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

    public T? FindBy(Func<T, bool> condition)
    {
        var artista = _context.Set<T>().FirstOrDefault(condition);
        return artista;
    }

    public IEnumerable<T> ListarPor(Func<T, bool> condicao)
    {
        return _context.Set<T>().Where(condicao);
    }

}


/*
 
 * No C#, trabalhamos com o Func para encapsular um método que geralmente tem um parâmetro e retorna um valor do tipo especificado pelo parâmetro TResult.
 
 * Ele é muito útil para situações como a que passamos no vídeo anterior, quando precisamos representar um método sem ter que passar especificamente valores
 * personalizados, deixando a correspondência para a assinatura do método.
 
 * Existem outras variações de utilização do Func, com mais parâmetros e condições, por exemplo, e para conhecer mais a fundo sobre eles e ver exemplos de
 * aplicação você pode consultar a documentação oficial (https://learn.microsoft.com/pt-br/dotnet/api/system.func-2?view=net-8.0).
 
 */