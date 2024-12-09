
using ScreenSound.Modelos;

namespace ScreenSound.Data
{
    internal abstract class DAL<T>
    {

        private readonly ScreenSoundContext _context;

        protected DAL(ScreenSoundContext context)
        {
            _context = context;
        }

        public abstract IEnumerable<T> Listar();
        public abstract void Adicionar(T objeto);
        public abstract void Atualizar(T objeto);
        public abstract void Excluir(T objeto);

    }
}
