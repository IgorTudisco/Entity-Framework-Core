﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Data;
using ScreenSound.Modelos;
using ScreenSound.Shared.Models.Models;

namespace ScreenSound.API.Endpoints;

public static class MusicasExtensions
{

    public static void AddEndPointArtistas(this WebApplication app)
    {
        app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
        {
            var listMusicas = EntityListToResponseList(dal.Listar(includes: m => m.Artista));
            return Results.Ok(listMusicas);
        });

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) =>
        {
            var musica = dal.FindBy(m => m.Nome.ToUpper().Equals(nome.ToUpper()), m => m.Artista).FirstOrDefault();
            if (musica is null)
            {
                return Results.NotFound();
            }

            var idArtista = musica.Artista?.Id;
            if (idArtista is null) return Results.NotFound();

            var responseMusica = EntityToResponse(musica);
            return Results.Ok(responseMusica);
        });

        app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) =>
        {
            var musica = new Musica(musicaRequest.nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.anoLancamento,
                Generos = musicaRequest.Generos is not null ? GeneroRequestConverter(musicaRequest.Generos) : new List<Genero>()
            };

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

    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos)
    {
        return generos.Select(a => RequestToEntity(a)).ToList();
    }

    private static Genero RequestToEntity(GeneroRequest genero)
    {
        return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
    }
    private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
    {
        return musicaList.Select(a => EntityToResponse(a)).ToList();
    }

    private static MusicaResponse EntityToResponse(Musica musica)
    {
        return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
    }
}
