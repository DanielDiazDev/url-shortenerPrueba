using System.Text;
using UrlShortener.Models;
using Newtonsoft.Json.Linq;

namespace UrlShortener.Service;

public class UrlService : IUrlShorteningService
{
    private readonly string _bitlyApiKey;

    public UrlService(string bitlyApiKey)
    {
        _bitlyApiKey = bitlyApiKey;
    }

    public async Task<Url> GetShortUrl(string originalUrl)
    {
        using HttpClient client = new HttpClient();
        string bitlyApiUrl = $"https://api-ssl.bitly.com/v4/shorten?domain=bit.ly";

        var requst = new HttpRequestMessage(HttpMethod.Post, bitlyApiUrl);
        requst.Headers.Add("Authorization", $"Bearer {_bitlyApiKey}");
        requst.Content = new StringContent($"{{\"long_url\": \"{originalUrl}\"}}",
            Encoding.UTF8,
            "application/json");

        var response = await client.SendAsync(requst);
        string jsonResponse = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(jsonResponse);

        Url result = new Url
        {
            OriginalUrl = originalUrl,
            ShortenedUrl = json["link"].ToString()
        };
        return result;
    }
}