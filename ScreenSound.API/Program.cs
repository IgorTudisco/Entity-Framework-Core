using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.Data;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Models.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Está sendo rodado localmente e a chave Secret não existe mais.

//builder.Host.ConfigureAppConfiguration(config =>
//{
//    var settings = config.Build();
//config.AddAzureAppConfiguration("Endpoint=https://screensound-congiguration.azconfig.io;Id=cXz/;Secret=BJNde2oveaWFGRhzgMTJkVFIUck7ypOw2PU6kHQJ0EO8ygbtI6oBJQQJ99BAACZoyfiyStiwAAACAZAC2br6");
//});


/*
 
 * Foi aumentada a restrição de acesso entre a aplicação Web e a API através da configuração CORS,
 * nomeada abaixo como wasm. Antes era permitido qualquer acesso, enquanto o código abaixo
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

app.MapGroup("auth").MapIdentityApi<PessoaComAcesso>().WithTags("Autorização");

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
 
 * API é a sigla referente à Application Programming Interface ou Interface de Programação de Aplicações e é o conjunto de regras e definições que
 * permite que softwares diferentes se comuniquem entre si. Sendo assim, API pode ser definido como o contrato que define as maneiras pelas quais
 * diferentes componentes de software devem interagir, facilitando a integração e a troca de dados entre sistemas distintos.
 
 * Existem diversos tipos de APIs, incluindo a que utilizamos durante o curso que é a API Web. Ela permite que serviços online forneçam dados e
 * funcionalidades para outros aplicativos através do protocolo HTTP e são essenciais para a criação de aplicações atuais, pois permitem a integração
 * de diferentes serviços e a construção de soluções mais robustas e flexíveis.
 
 * Para conhecer mais sobre APIs e entender seus diversos tipos e aplicações, deixamos como sugestão de leitura a documentação da AWS sobre APIs,
 * que traz de forma bem acessível uma boa base para compreender esse assunto. (https://aws.amazon.com/pt/what-is/api/)
 
 */