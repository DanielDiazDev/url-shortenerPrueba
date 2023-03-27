using UrlShortener.Models;

namespace UrlShortener.Service;

public interface IUrlShorteningService
{
    Task<Url> GetShortUrl(string? originalUrl);
}