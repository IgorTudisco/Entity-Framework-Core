using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.Data;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Models.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Est� sendo rodado localmente e a chave Secret n�o existe mais.

//builder.Host.ConfigureAppConfiguration(config =>
//{
//    var settings = config.Build();
//config.AddAzureAppConfiguration("Endpoint=https://screensound-congiguration.azconfig.io;Id=cXz/;Secret=BJNde2oveaWFGRhzgMTJkVFIUck7ypOw2PU6kHQJ0EO8ygbtI6oBJQQJ99BAACZoyfiyStiwAAACAZAC2br6");
//});


/*
 
 * Foi aumentada a restri��o de acesso entre a aplica��o Web e a API atrav�s da configura��o CORS,
 * nomeada abaixo como wasm. Antes era permitido qualquer acesso, enquanto o c�digo abaixo
 * somente permite origens apontadas pelas URLs dos dois projetos.
 
 */

builder.Services.AddCors(
    options => options.AddPolicy(

        "wasm",

        policy => policy.WithOrigins(

            [builder.Configuration["BackendUrl"] ?? "https://localhost:7122/",
            builder.Configuration["FrontendUrl"] ?? "https://localhost:7199/"]

        ).AllowAnyMethod()
         .SetIsOriginAllowed(pol => true)
         .AllowAnyHeader()
         .AllowCredentials()
    )
);


builder.Services.AddDbContext<ScreenSoundContext>((options) =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ScreenSoundDB"]).UseLazyLoadingProxies(false);
});

builder.Services.AddIdentityApiEndpoints<PessoaComAcesso>().AddEntityFrameworkStores<ScreenSoundContext>();

builder.Services.AddAuthorization();

builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("wasm");

app.AddEndPointArtistas();
app.AddEndPointMusicas();
app.AddEndPointGenero();

app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autoriza��o");

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials()
);

app.UseStaticFiles();

app.Run();


/*
 
 * API � a sigla referente � Application Programming Interface ou Interface de Programa��o de Aplica��es e � o conjunto de regras e defini��es que
 * permite que softwares diferentes se comuniquem entre si. Sendo assim, API pode ser definido como o contrato que define as maneiras pelas quais
 * diferentes componentes de software devem interagir, facilitando a integra��o e a troca de dados entre sistemas distintos.
 
 * Existem diversos tipos de APIs, incluindo a que utilizamos durante o curso que � a API Web. Ela permite que servi�os online forne�am dados e
 * funcionalidades para outros aplicativos atrav�s do protocolo HTTP e s�o essenciais para a cria��o de aplica��es atuais, pois permitem a integra��o
 * de diferentes servi�os e a constru��o de solu��es mais robustas e flex�veis.
 
 * Para conhecer mais sobre APIs e entender seus diversos tipos e aplica��es, deixamos como sugest�o de leitura a documenta��o da AWS sobre APIs,
 * que traz de forma bem acess�vel uma boa base para compreender esse assunto. (https://aws.amazon.com/pt/what-is/api/)
 
 */