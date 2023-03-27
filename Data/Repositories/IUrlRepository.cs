using UrlShortener.Models;

namespace Data.Repositories;

public interface IUrlRepository
{
    Task<IEnumerable<Url?>> GetAll();
    Task<Url?> GetById(Guid id);
    Task Add(Url url);
    Task Update(Url url);
    Task Delete(Guid id);
}
