using Azure.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.API.Responses;
using ScreenSound.Data;
using ScreenSound.Migrations;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Models.Models;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace ScreenSound.API.Endpoints;

public static class ArtistasExtensions
{
    public static void AddEndPointMusicas(this WebApplication app)
    {
        // Cria um grupo de endpoints para a rota /Artistas
        var groupBuilder = app.MapGroup("artistas").RequireAuthorization().WithTags("Artistas");

        // Adiciona um endpoint para a rota /artistas e não é necessário passar o nome do artista. Se não ficaria /artistas/Artistas, mas só queremos /artistas.
        groupBuilder.MapGet("", ([FromServices] DAL<Artista> dal, [FromServices] DAL<Artista> dalArtista) =>
        {
            var listArtista = EntityListToResponseList(dal.Listar(), dalArtista);
            return Results.Ok(listArtista);
        });

        groupBuilder.MapGet("/{nome}", ([FromServices] DAL<Artista> dal, string nome, [FromServices] DAL<Artista> dalArtista) =>
        {
            var artista = dal.FindBy(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }
            var responseArtista = EntityToResponse(artista, dalArtista);
            return Results.Ok(responseArtista);
        });

        groupBuilder.MapPost("", async ([FromServices]IHostEnvironment env, [FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) =>
        {
            var nome = artistaRequest.nome.Trim();
            var imagemArtista = DateTime.Now.ToString("ddMMyyyyhhss") + "." + nome + ".jpeg";

            var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotoPerfil", imagemArtista);

            using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.fotoPerfil!));
            using FileStream fs = new(path, FileMode.Create);
            await ms.CopyToAsync(fs);

            var artista = new Artista(artistaRequest.nome, artistaRequest.bio)
            {
                FotoPerfil = $"/FotoPerfil/{imagemArtista}"
            };

            dal.Adicionar(artista);
            return Results.Ok(artista);
        });

        groupBuilder.MapDelete("{id}", ([FromServices] DAL<Artista> dal, int id) =>
        {
            var artista = dal.FindBy(a => a.Id == id);
            if (artista is null) return Results.NotFound();

            dal.Excluir(artista);
            return Results.NoContent();

        });

        groupBuilder.MapPut("", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequestEdit artistaRequestEdit) =>
        {
            var artistaAtualizar = dal.FindBy(a => a.Id == artistaRequestEdit.Id);

            if (artistaAtualizar is null) return Results.NotFound();

            artistaAtualizar.Nome = artistaRequestEdit.nome;
            artistaAtualizar.Bio = artistaRequestEdit.bio;
            dal.Atualizar(artistaAtualizar);

            return Results.Ok();

        });

        // Rota que permite avaliar um artista
        groupBuilder.MapPost("avaliacao", (HttpContext context, [FromBody] AvaliacaoArtistaRequest request, [FromServices] DAL<Artista> dalArtista, [FromServices] DAL<PessoaComAcesso> dalPessoaComAcesso, [FromServices] DAL<AvaliacaoArtista> dalAvaliacaoArtista) =>
        {
            // Updated code to use DAL's Listar method with includes instead of 'Include' on the entity.
            var artista = dalArtista.Listar(a => a.Avaliacoes).FirstOrDefault(a => a.Id == request.ArtistaId);

            if (artista is null) return Results.NotFound();

            var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                ?? throw new InvalidOperationException("Usuário não autenticado");

            var pessoa = dalPessoaComAcesso.FindBy(p => p.Email!.Equals(email))
                ?? throw new InvalidOperationException("Pessoa não encontrada");

            var avaliacao = artista.Avaliacoes.FirstOrDefault(a => a.ArtistaId == artista.Id && a.PessoaId == pessoa.Id);

            if (avaliacao is null)
            {
                artista.AdicionarNota(pessoa.Id, request.Nota);
            }
            else
            {
                avaliacao.Nota = request.Nota;
            }

            dalArtista.Atualizar(artista);

            return Results.Created();
        });

        // Rota que busca a nota do artista
        groupBuilder.MapGet("{id}/avaliacao", (int id, HttpContext context, [FromServices] DAL<Artista> dalArtista, [FromServices] DAL<PessoaComAcesso> dalPessoa, [FromServices] DAL<AvaliacaoArtista> dalAvaliacaoArtista) =>
        {
            // Valida se o artista existe
            var artista = dalArtista.FindBy(a => a.Id == id, a => a.Avaliacoes).FirstOrDefault();
            // se em algum momento você quiser incluir também o Artista dentro de cada avaliação, adicione um include aninhado assim: a => a.Avaliacoes, a => a.Avaliacoes.Select(av => av.Artista)
            if (artista is null) return Results.NotFound();

            // Verifica se a pessoa está autenticada
            var email = context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            // Verifica se a pessoa existe
            var pessoa = dalPessoa.FindBy(p => p.Email!.Equals(email))
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            // Busca a avaliação do artista feita pela pessoa, passando o id do artista e o id da pessoa
            var avaliacao = artista.Avaliacoes.FirstOrDefault(a => a.ArtistaId == id && a.PessoaId == pessoa.Id);

            // Se a avaliação não existir, retorna 0
            if (avaliacao is null)
            {
                // Retorna a nota 0
                return Results.Ok(new AvaliacaoArtistaResponse(id, 0));
            }
            else
            {
                // Se a avaliação existir, retorna a nota
                return Results.Ok(new AvaliacaoArtistaResponse(id, avaliacao.Nota));
            }

        });

    }

    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas, [FromServices] DAL<Artista> dalArtista)
    {
        return listaDeArtistas.Select(a => EntityToResponse(a, dalArtista)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista artista, [FromServices] DAL<Artista> dalArtista)
    {
        var avaliacoes = dalArtista.Listar(a => a.Avaliacoes).FirstOrDefault(a => a.Id == artista.Id)?.Avaliacoes;

        var averageNota = new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil)
        {
            Classificacao = avaliacoes?.Select(a => a.Nota).Average() ?? 0
        };

        return averageNota;
    }

}


/*
 * 
 * Dúvidas sobre o include:
 * 
 * https://cursos.alura.com.br/forum/topico-duvida-nao-estou-conseguindo-persistir-a-nota-no-meu-banco-481357#1766252
 * 
 */