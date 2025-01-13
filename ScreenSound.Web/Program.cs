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
 
 * O MudBlazor é uma biblioteca de componentes para utilização com o ASP.NET Blazor. Como qualquer biblioteca, seus objetivos incluem
 * facilitar o desenvolvimento na utilização do framework e prover produtividade à pessoa desenvolvedora.
 
 * A biblioteca se baseia no Material Design, um framework de design muito popular, e entrega uma série de componentes prontos para uso.
 * Seguem, abaixo, algumas características do MudBlazor:
 
 * Componentes Material Design: o MudBlazor oferece uma gama de componentes com base no Material Design, como: botões, cards,
 * formulários, selects, barras de progresso e muito mais. Isso agiliza muito a criação de interfaces modernas e visualmente
 * atraentes por pessoas desenvolvedoras.
 
 * Facilidade de Customização: Mesmo oferecendo componentes com uma estética Material Design, o MudBlazor é customizável. Podemos ajustar cores,
 * estilos e comportamentos dos componentes de acordo com as necessidades da aplicação.
 
 * Suporte Responsivo: Todos os componentes do MudBlazor são projetados para suportar uma experiência responsiva em diversos dispositivos.
 
 * Integração com Blazor: Sendo uma biblioteca, é construída especificamente para o Blazor, facilitando a integração com outros componentes
 * e funcionalidades já presentes no framework.
 
 * Suporte a Temas: Com o MudBlazor, a troca de temas é bem simples, proporcionando uma flexibilidade adicional para adaptar a aparência das
 * aplicações de acordo com os requisitos específicos do projeto.
 
 * Documentação: A documentação do MudBlazor está em constante atualização e nos exemplifica muito bem a utilização dos componentes, a instalação
 * da biblioteca no projeto, entre outros.
 
 * Por simplificar muito o desenvolvimento de interfaces Web Blazor, esta foi a opção para este curso, mas lembre-se que existem outras bibliotecas
 * disponíveis também com a mesma finalidade. Para saber mais sobre a biblioteca, recomendamos o acesso ao link da Documentação MudBlazor
 * (https://mudblazor.com/docs/overview).
 
 */