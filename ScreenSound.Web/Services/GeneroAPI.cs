using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class GeneroAPI
{
    private readonly HttpClient _httpClient;

    public GeneroAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<GeneroResponse>?> GetGenerosAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<GeneroResponse>>("generos");
    }

    public async Task<GeneroResponse?> GetGeneroPorNomeAsync(string nome)
    {
        return await _httpClient.GetFromJsonAsync<GeneroResponse>($"generos/{nome}");
    }

    public async Task AddGeneroAsync(GeneroRequest generoRequest)
    {
        await _httpClient.PostAsJsonAsync("genero", generoRequest);
    }

    public async Task EditGeneroAsync(GeneroRequestEdit generoRequestEdit)
    {
        await _httpClient.PutAsJsonAsync($"genero", generoRequestEdit);
    }

    public async Task DeleteGeneroAsync(int id)
    {
        await _httpClient.DeleteAsync($"genero/{id}");
    }
}