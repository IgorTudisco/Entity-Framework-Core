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

// Injetando a minha autentica��o e passando o escopo por onde minha aplica��o ser� autenticada
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthAPI>();

// inserindo o provider autenticado no meu escopo
builder.Services.AddScoped<AuthAPI>(sp => (AuthAPI) sp.GetRequiredService<AuthenticationStateProvider>());

/*
 
 * Foi alterado o tipo de inje��o de transient para scoped. O motivo � conseguir reaproveitar os objetos do mesmo tipo em diferentes p�ginas
 * e componentes, dentro da mesma requisi��o. Esse tipo de ciclo de vida dos objetos injetados (scoped) ser� fundamental para a autentica��o.
 
 */

builder.Services.AddScoped<ArtistasAPI>();
builder.Services.AddScoped<MusicasAPI>();
builder.Services.AddScoped<GeneroAPI>();
builder.Services.AddScoped<CookieHandler>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["APIServer:Url"]!);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).AddHttpMessageHandler<CookieHandler>(); // Faz como que todas as rotas usem o cookie de autentica��o (.AddHttpMessageHandler<CookieHandler>();)

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

/*
 
 * No ASP.NET existem os m�todos de extens�o AddScoped, AddTransient e AddSingleton que fazem parte do sistema
 * de inje��o de depend�ncia no ASP.NET Core. Eles s�o usados para registrar servi�os no cont�iner de inje��o
 * de depend�ncia nativo do framework, e s�o respons�veis por controlar o ciclo de vida das inst�ncias desses servi�os.
 
 * AddScoped
 * No m�todo AddScoped registramos um servi�o com um tempo de vida por escopo. Isso significa dizer que uma inst�ncia do
 * servi�o ser� criada e mantida durante todo o ciclo de vida de uma �nica requisi��o HTTP (ou escopo) e para cada nova requisi��o
 * recebe sua pr�pria inst�ncia do servi�o.
 
 * AddTransient
 
 * J� no m�todo AddTransient o servi�o � registrado com um tempo de vida transit�rio, ou seja, uma nova inst�ncia do servi�o ser�
 * criada toda vez que ele for solicitado. Isso pode acontecer v�rias vezes durante a mesma requisi��o ou em diferentes requisi��es.
 
 * AddSingleton
 
 * Para o m�todo AddSingleton o servi�o � registrado com um tempo de vida �nico em toda a aplica��o. Apenas uma inst�ncia do servi�o
 * ser� criada e compartilhada por todas as requisi��es e threads durante a execu��o do aplicativo.
 
 * Documenta��o oficial - Inje��o de depend�ncia no ASP.NET Core https://learn.microsoft.com/pt-br/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-9.0
 
 * Documenta��o oficial - Inje��o de depend�ncia do .NET https://learn.microsoft.com/pt-br/dotnet/core/extensions/dependency-injection
 
 */