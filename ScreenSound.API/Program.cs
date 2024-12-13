using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) =>
{
    return Results.Ok(dal.Listar());
});

app.MapGet("/Artistas/{nome}", ([FromServices] DAL < Artista > dal, string nome) =>
{
    var artista = dal.FindBy(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
    if (artista is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(artista);
});

app.MapPost("/Artistas", ([FromServices] DAL < Artista > dal, [FromBody] Artista artista) =>
{
    dal.Adicionar(artista);
    return Results.Ok(artista);

});

app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
{
    var artista = dal.FindBy(a => a.Id == id);
    if(artista is null) return Results.NotFound();

    dal.Excluir(artista);
    return Results.NoContent();

});

app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] Artista artista) =>
{
    var artistaAtualizar = dal.FindBy(a => a.Id == artista.Id);

    if (artistaAtualizar is null) return Results.NotFound();

    artistaAtualizar.Nome = artista.Nome;
    artistaAtualizar.Bio = artista.Bio;
    artistaAtualizar.FotoPerfil = artista.FotoPerfil;

    return Results.Ok();

});

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

app.MapPost("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
{
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

app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) =>
{
    var musicaAtualizar = dal.FindBy(a => a.Id == musica.Id);

    if (musicaAtualizar is null) return Results.NotFound();

    musicaAtualizar.Nome = musica.Nome;
    musicaAtualizar.AnoLancamento = musica.AnoLancamento;

    return Results.Ok();

});

app.Run();


/*
 
 * API é a sigla referente à Application Programming Interface ou Interface de Programação de Aplicações e é o conjunto de regras e definições que
 * permite que softwares diferentes se comuniquem entre si. Sendo assim, API pode ser definido como o contrato que define as maneiras pelas quais
 * diferentes componentes de software devem interagir, facilitando a integração e a troca de dados entre sistemas distintos.
 
 * Existem diversos tipos de APIs, incluindo a que utilizamos durante o curso que é a API Web. Ela permite que serviços online forneçam dados e
 * funcionalidades para outros aplicativos através do protocolo HTTP e são essenciais para a criação de aplicações atuais, pois permitem a integração
 * de diferentes serviços e a construção de soluções mais robustas e flexíveis.
 
 * Para conhecer mais sobre APIs e entender seus diversos tipos e aplicações, deixamos como sugestão de leitura a documentação da AWS sobre APIs,
 * que traz de forma bem acessível uma boa base para compreender esse assunto. (https://aws.amazon.com/pt/what-is/api/)
 
 */