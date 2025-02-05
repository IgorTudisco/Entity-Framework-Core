
using ScreenSound.Modelos;

namespace ScreenSound.Shared.Models.Models;

public class AvaliacaoArtista
{
    public int PessoaId { get; set; }
    public int ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public int Nota { get; set; }

}
