using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ScreenSound.Web;
using ScreenSound.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddTransient<ArtistasAPI>();
builder.Services.AddTransient<MusicasAPI>();
builder.Services.AddTransient<GeneroAPI>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

await builder.Build().RunAsync();


/*
 
 * O MudBlazor � uma biblioteca de componentes para utiliza��o com o ASP.NET Blazor. Como qualquer biblioteca, seus objetivos incluem
 * facilitar o desenvolvimento na utiliza��o do framework e prover produtividade � pessoa desenvolvedora.
 
 * A biblioteca se baseia no Material Design, um framework de design muito popular, e entrega uma s�rie de componentes prontos para uso.
 * Seguem, abaixo, algumas caracter�sticas do MudBlazor:
 
 * Componentes Material Design: o MudBlazor oferece uma gama de componentes com base no Material Design, como: bot�es, cards,
 * formul�rios, selects, barras de progresso e muito mais. Isso agiliza muito a cria��o de interfaces modernas e visualmente
 * atraentes por pessoas desenvolvedoras.
 
 * Facilidade de Customiza��o: Mesmo oferecendo componentes com uma est�tica Material Design, o MudBlazor � customiz�vel. Podemos ajustar cores,
 * estilos e comportamentos dos componentes de acordo com as necessidades da aplica��o.
 
 * Suporte Responsivo: Todos os componentes do MudBlazor s�o projetados para suportar uma experi�ncia responsiva em diversos dispositivos.
 
 * Integra��o com Blazor: Sendo uma biblioteca, � constru�da especificamente para o Blazor, facilitando a integra��o com outros componentes
 * e funcionalidades j� presentes no framework.
 
 * Suporte a Temas: Com o MudBlazor, a troca de temas � bem simples, proporcionando uma flexibilidade adicional para adaptar a apar�ncia das
 * aplica��es de acordo com os requisitos espec�ficos do projeto.
 
 * Documenta��o: A documenta��o do MudBlazor est� em constante atualiza��o e nos exemplifica muito bem a utiliza��o dos componentes, a instala��o
 * da biblioteca no projeto, entre outros.
 
 * Por simplificar muito o desenvolvimento de interfaces Web Blazor, esta foi a op��o para este curso, mas lembre-se que existem outras bibliotecas
 * dispon�veis tamb�m com a mesma finalidade. Para saber mais sobre a biblioteca, recomendamos o acesso ao link da Documenta��o MudBlazor
 * (https://mudblazor.com/docs/overview).
 
 */