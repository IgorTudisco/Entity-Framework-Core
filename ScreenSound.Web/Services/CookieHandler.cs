using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace ScreenSound.Web.Services
{
    public class CookieHandler : DelegatingHandler
    {
        override protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
