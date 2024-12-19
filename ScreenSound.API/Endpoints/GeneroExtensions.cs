using Microsoft.AspNetCore.Mvc;
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
        app.MapGet("/Generos", ([FromServices] DAL<Genero> dal) =>
        {
            var listGenero = EntityListToResponseList(dal.Listar());
            return Results.Ok(listGenero);
        });

        app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
        {
            var genero = dal.FindBy(g => g.Nome.ToUpper().Equals(nome.ToUpper()));
            if (genero is null)
            {
                return Results.NotFound();
            }
            var responseGenero = EntityToResponse(genero);
            return Results.Ok(responseGenero);
        });

        app.MapPost("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequest generoRequest) =>
        {
            var genero = new Genero()
            {
                Nome = generoRequest.nome,
                Descricao = generoRequest.descricao
            };
            dal.Adicionar(genero);
            return Results.Ok(genero);
        });

        app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> dal, int id) =>
        {
            var genero = dal.FindBy(g => g.Id == id);
            if (genero is null) return Results.NotFound();

            dal.Excluir(genero);
            return Results.NoContent();

        });

        app.MapPut("/Generos", ([FromServices] DAL<Genero> dal, [FromBody] GeneroRequestEdit generoRequestEdit) =>
        {
            var generoAtualizar = dal.FindBy(g => g.Id == generoRequestEdit.Id);

            if (generoAtualizar is null) return Results.NotFound();

            generoAtualizar.Nome = generoRequestEdit.nome;
            generoAtualizar.Descricao = generoRequestEdit.descricao;
            dal.Atualizar(generoAtualizar);

            return Results.Ok();

        });
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
