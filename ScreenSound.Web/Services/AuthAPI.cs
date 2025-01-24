using Microsoft.AspNetCore.Components.Authorization;
using ScreenSound.Web.Responses;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ScreenSound.Web.Services;

public class AuthAPI(IHttpClientFactory factory) : AuthenticationStateProvider
{
    private readonly HttpClient _httpClient = factory.CreateClient("API");

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var pessoa = new ClaimsPrincipal(); // quando não estiver autenticado ele retorna um objeto vazio ou seja anonimo.

        var response = await _httpClient.GetAsync("auth/manage/info");

        if (response.IsSuccessStatusCode)
        {
            var info = await response.Content.ReadFromJsonAsync<InfoPessoaResponse>();

            Claim[] dados =
            [
                new Claim(ClaimTypes.Name, info!.Email!),
                new Claim(ClaimTypes.Email, info!.Email!)
            ];

            var identity = new ClaimsIdentity(dados, "Cookies");
            pessoa = new ClaimsPrincipal(identity);
        }

        return new AuthenticationState(pessoa);
    }

    public async Task<AuthResponse> LoginAsync(string email, string senha)
    {
        var response = await _httpClient.PostAsJsonAsync("auth/login?useCookies=true", new
        {
            email,
            password = senha
        });

        if (response.IsSuccessStatusCode)
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            return new AuthResponse { Sucesso = true };
        }

        return new AuthResponse { Sucesso = false, Erros = ["Login/senha inválidos"] };
    }
}

/*
 
 * Claims:
 
 * Veja todos os tipos comuns especificados na documentação oficial
 * sobre ClaimTypes Class. https://learn.microsoft.com/en-us/dotnet/api/system.security.claims.claimtypes?view=net-9.0
 
 * - ClaimsIdentity:  Documentação oficial sobre ClaimsIdentity Class. https://learn.microsoft.com/en-us/dotnet/api/system.security.claims.claimsidentity?view=net-9.0
 
 * - ClaimsPrincipal: Documentação oficial sobre ClaimsPrincipal Class. https://learn.microsoft.com/pt-br/dotnet/api/system.security.principal.iprincipal?view=net-9.0
 
 * Artigo Autenticação e autorização em aplicações Blazor https://learn.microsoft.com/pt-br/aspnet/core/blazor/security/?view=aspnetcore-9.0&tabs=visual-studio
 
 * Artigo Sharepoint - Definições de termos de identidade baseadas em declarações https://learn.microsoft.com/pt-br/sharepoint/dev/general-development/claims-based-identity-term-definitions
 
 * Artigo Introduction to Authentication with ASP.NET Core (em inglês) https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
 
 */