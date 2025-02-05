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
        groupBuilder.MapGet("", ([FromServices] DAL<Artista> dal) =>
        {
            var listArtista = EntityListToResponseList(dal.Listar());
            return Results.Ok(listArtista);
        });

        groupBuilder.MapGet("/{nome}", ([FromServices] DAL<Artista> dal, string nome) =>
        {
            var artista = dal.FindBy(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (artista is null)
            {
                return Results.NotFound();
            }
            var responseArtista = EntityToResponse(artista);
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
        groupBuilder.MapPost("avaliacao", (HttpContext context, [FromBody] AvaliacaoArtistaRequest request, [FromServices] DAL<Artista> dalArtista, [FromServices] DAL<PessoaComAcesso> dalPessoaComAcesso, DAL<AvaliacaoArtista> dalAvaliacaoArtista) =>
        {
            var artista = dalArtista.FindBy(a => a.Id == request.ArtistaId);

            if (artista is null) return Results.NotFound();

            // O ?? é um operador de coalescência nula, que retorna o valor da esquerda se não for nulo, senão o valor da direita
            var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value
                ?? throw new InvalidOperationException("Usuário não autenticado");

            // Se o email não for encontrado, lança uma exceção de InvalidOperationException com a mensagem "Pessoa não encontrada"
            var pessoa = dalPessoaComAcesso.FindBy(p => p.Email!.Equals(email))
                ?? throw new InvalidOperationException("Pessoa não encontrada");

            // Verifica se a pessoa já avaliou o artista, passando o id do artista e o id da pessoa
            var avaliacao = artista.Avaliacoes.FirstOrDefault(a => a.ArtistaId == artista.Id && a.PessoaId == pessoa.Id);

            var dalAvaliacao = dalAvaliacaoArtista.FindBy(av => av.ArtistaId == artista.Id && av.PessoaId == pessoa.Id);

            avaliacao = dalAvaliacao;

            // Se a avaliação não existir, adiciona a nota
            if (avaliacao is null)
            {
                // Adiciona a nota
                artista.AdicionarNota(pessoa.Id, request.Nota);
            }
            else
            {
                // Se a avaliação existir, atualiza a nota
                avaliacao.Nota = request.Nota;                
            }

            // Se a avaliação existir, atualiza a nota
            dalArtista.Atualizar(artista);

            return Results.Created();
        });

        // Rota que busca a nota do artista
        groupBuilder.MapGet("{id}/avaliacao", (int id, HttpContext context, [FromServices] DAL<Artista> dalArtista, [FromServices] DAL<PessoaComAcesso> dalPessoa, [FromServices] DAL<AvaliacaoArtista> dalAvaliacaoArtista) =>
        {
            // Valida se o artista existe
            var artista = dalArtista.FindBy(a => a.Id == id);
            if (artista is null) return Results.NotFound();

            // Verifica se a pessoa está autenticada
            var email = context.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            // Verifica se a pessoa existe
            var pessoa = dalPessoa.FindBy(p => p.Email!.Equals(email))
                ?? throw new InvalidOperationException("Não foi encontrado o email da pessoa logada");

            // Busca a avaliação do artista feita pela pessoa, passando o id do artista e o id da pessoa
            var avaliacao = artista.Avaliacoes.FirstOrDefault(a => a.ArtistaId == id && a.PessoaId == pessoa.Id);

            var dalAvaliacao = dalAvaliacaoArtista.FindBy(av => av.ArtistaId == id && av.PessoaId == pessoa.Id);

            avaliacao = dalAvaliacao;

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

    private static ICollection<ArtistaResponse> EntityListToResponseList(IEnumerable<Artista> listaDeArtistas)
    {
        return listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ArtistaResponse EntityToResponse(Artista artista)
    {
        return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil)
        {
            Classificacao = artista.Avaliacoes.Select(a => a.Nota).DefaultIfEmpty(0).Average()
        };
    }
}
