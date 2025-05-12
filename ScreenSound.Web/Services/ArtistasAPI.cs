using ScreenSound.Web.Requests;
using ScreenSound.Web.Response;
using ScreenSound.Web.Responses;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services;

public class ArtistasAPI
{
    private readonly HttpClient _httpClient;

    public ArtistasAPI(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<ArtistaResponse>?> GetArtistasAsync()
    {
        return await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
    }

    public async Task AddArtistaAsync(ArtistaRequest artistaRequest)
    {
        await _httpClient.PostAsJsonAsync("artistas", artistaRequest);
    }

    public async Task DeleteArtistaAsync(int id)
    {
        await _httpClient.DeleteAsync($"artistas/{id}");
    }

    public async Task EditArtistaAsync(ArtistaRequestEdit artistaRequestEdit)
    {
        await _httpClient.PutAsJsonAsync("artistas", artistaRequestEdit);
    }

    public async Task<ArtistaResponse?> GetArtistaPorNomeAsync(string nome)
    {
        return await _httpClient.GetFromJsonAsync<ArtistaResponse>($"artistas/{nome}");
    }

    public async Task AvaliaArtistaAsync(int artistaId, double nota)
    {
        await _httpClient.PostAsJsonAsync("artistas/avaliacao", new { artistaId, nota });
    }

    public async Task<AvaliacaoDoArtistaResponse?>
        GetAvaliacaoDaPessoaLogadaAsync(int artistaId)
    {
        return await _httpClient.GetFromJsonAsync<AvaliacaoDoArtistaResponse?>($"{artistaId}/avaliacao");
    }
}
