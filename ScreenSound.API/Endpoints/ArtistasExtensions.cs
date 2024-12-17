using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Data;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndPointMusicas(this WebApplication app)
    {
        app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.FindBy(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }
            return Results.Ok(artista);
        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var artista = new Artista(artistaRequest.nome, artistaRequest.bio);
            dal.Adicionar(artista);
            return Results.Ok(artista);
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.FindBy(a => a.Id == id);
            if (artista is null) return Results.NotFound();

            dal.Excluir(artista);
            return Results.NoContent();

        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
        {
            var artistaAtualizar = dal.FindBy(a => a.Id == artistaRequestEdit.Id);

            if (artistaAtualizar is null) return Results.NotFound();

            artistaAtualizar.Nome = artistaRequestEdit.nome;
            artistaAtualizar.Bio = artistaRequestEdit.bio;
            dal.Atualizar(artistaAtualizar);

            return Results.Ok();

        });
    }
}
