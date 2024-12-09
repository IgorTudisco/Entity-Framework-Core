
using ScreenSound.Modelos;

namespace ScreenSound.Data
{
    internal abstract class DAL<T> where T : class // Indicando que a ref de T será uma classe.
    {

        private readonly ScreenSoundContext _context;

        protected DAL(ScreenSoundContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Listar()
        {
            return _context.Set<T>().ToList(); // O Set vai consegui pegar o tipo genérico que será usado. (Temos que indicar que a ref será uma classe)
        }

        public abstract void Adicionar(T objeto);
        public abstract void Atualizar(T objeto);
        public abstract void Excluir(T objeto);

    }
}
