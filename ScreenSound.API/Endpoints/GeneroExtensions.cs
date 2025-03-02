﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Modelos;
using ScreenSound.Shared.Models.Models;

namespace ScreenSound.API.Endpoints;

public static class GeneroExtensions
{
    public static void AddEndPointGenero(this WebApplication app)
    {
        var groupBuilder = app.MapGroup("genero").RequireAuthorization().WithTags("Genero");

        groupBuilder.MapGet("", ([FromServices] DAL<Genero> dal) =>
        {
            var listGenero = EntityListToResponseList(dal.Listar());
            return Results.Ok(listGenero);
        });

        groupBuilder.MapGet("{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
        {
            var genero = dal.FindBy(g => g.Nome!.ToUpper().Equals(nome.ToUpper()));
            if (genero is null)
            {
                return Results.NotFound("Gênero não encontrado.");
            }
            var responseGenero = EntityToResponse(genero);
            return Results.Ok(responseGenero);
        });

        groupBuilder.MapPost("", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
        {
            var genero = RequestToEntity(generoRequest);
            dal.Adicionar(genero);
            return Results.Ok(genero);
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Genero> dal, int id) =>
        {
            var genero = dal.FindBy(g => g.Id == id);
            if (genero is null) return Results.NotFound("Gênero para exclusão não encontrado.");

            dal.Excluir(genero);
            return Results.NoContent();

        });

        groupBuilder.MapPut("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit generoRequestEdit) =>
        {
            var generoAtualizar = dal.FindBy(g => g.Id == generoRequestEdit.Id);

            if (generoAtualizar is null) return Results.NotFound();

            generoAtualizar.Nome = generoRequestEdit.nome;
            generoAtualizar.Descricao = generoRequestEdit.descricao;
            dal.Atualizar(generoAtualizar);

            return Results.Ok();

        });
    }

    private static Genero RequestToEntity(GeneroRequest generoRequest)
    {
        return new Genero() { Nome = generoRequest.nome, Descricao = generoRequest.descricao };
    }

    private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> listaDeGeneros)
    {
        return listaDeGeneros.Select(g => EntityToResponse(g)).ToList();
    }

    private static GeneroResponse EntityToResponse(Genero genero)
    {
        return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
    }
}
