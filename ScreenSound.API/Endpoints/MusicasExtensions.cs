using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class MusicasExtensions
{

    public static void AddEndPointArtistas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var musica = dal.FindBy(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (musica is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(musica);
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
        {
            var musica = new Musica(musicaRequest.nome);
            musica.Nome = musicaRequest.nome;
            musica.AnoLancamento = musicaRequest.anoLancamento;
            dal.Adicionar(musica);
            return Results.Ok();
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) =>
        {
            var musica = dal.FindBy(a => a.Id == id);
            if (musica is null) return Results.NotFound();

            dal.Excluir(musica);
            return Results.NoContent();

        });

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> dalMusica, [FromServices] DAL<Artista> dalArtista, [FromBody] MusicaRequestEdit musicaRequestEdit) =>
        {
            var musicaAtualizar = dalMusica.FindBy(m => m.Id == musicaRequestEdit.Id);
            if (musicaAtualizar is null) return Results.NotFound();

            var atualizarMusicaArtista = dalArtista.FindBy(a => a.Id == musicaRequestEdit.ArtistaId);
            if (atualizarMusicaArtista is null) return Results.NotFound();

            musicaAtualizar.Nome = musicaRequestEdit.nome;
            musicaAtualizar.AnoLancamento = musicaRequestEdit.anoLancamento;
            musicaAtualizar.Artista = atualizarMusicaArtista;

            dalMusica.Atualizar(musicaAtualizar);

            return Results.Ok();

        });
    }
}
