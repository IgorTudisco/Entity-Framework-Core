using Microsoft.AspNetCore.Mvc;
using ScreenSound.Data;
using ScreenSound.Modelos;
using System.Data.SqlTypes;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();

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