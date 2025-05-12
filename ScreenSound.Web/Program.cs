using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ScreenSound.Web;
using ScreenSound.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

// Injetando a minha autenticação e passando o escopo por onde minha aplicação será autenticada
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthAPI>();

// inserindo o provider autenticado no meu escopo
builder.Services.AddScoped<AuthAPI>(sp => (AuthAPI) sp.GetRequiredService<AuthenticationStateProvider>());

/*
 
 * Foi alterado o tipo de injeção de transient para scoped. O motivo é conseguir reaproveitar os objetos do mesmo tipo em diferentes páginas
 * e componentes, dentro da mesma requisição. Esse tipo de ciclo de vida dos objetos injetados (scoped) será fundamental para a autenticação.
 
 */

builder.Services.AddScoped<ArtistasAPI>();
builder.Services.AddScoped<MusicasAPI>();
builder.Services.AddScoped<GeneroAPI>();
builder.Services.AddScoped<CookieHandler>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<CookieHandler>(); // Faz como que todas as rotas usem o cookie de autenticação (.AddHttpMessageHandler<CookieHandler>();)

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

/*
 
 * No ASP.NET existem os métodos de extensão AddScoped, AddTransient e AddSingleton que fazem parte do sistema
 * de injeção de dependência no ASP.NET Core. Eles são usados para registrar serviços no contêiner de injeção
 * de dependência nativo do framework, e são responsáveis por controlar o ciclo de vida das instâncias desses serviços.
 
 * AddScoped
 * No método AddScoped registramos um serviço com um tempo de vida por escopo. Isso significa dizer que uma instância do
 * serviço será criada e mantida durante todo o ciclo de vida de uma única requisição HTTP (ou escopo) e para cada nova requisição
 * recebe sua própria instância do serviço.
 
 * AddTransient
 
 * Já no método AddTransient o serviço é registrado com um tempo de vida transitório, ou seja, uma nova instância do serviço será
 * criada toda vez que ele for solicitado. Isso pode acontecer várias vezes durante a mesma requisição ou em diferentes requisições.
 
 * AddSingleton
 
 * Para o método AddSingleton o serviço é registrado com um tempo de vida único em toda a aplicação. Apenas uma instância do serviço
 * será criada e compartilhada por todas as requisições e threads durante a execução do aplicativo.
 
 * Documentação oficial - Injeção de dependência no ASP.NET Core https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-9.0
 
 * Documentação oficial - Injeção de dependência do .NET https://learn.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection
 
 */