using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Requests;

public record GeneroRequest([Required] string nome, string descricao);
