using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace Data.Repositories;

public class UrlRepository : IUrlRepository
{
    private readonly UrlContext _context;

    public UrlRepository(UrlContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Url?>> GetAll()
    {
        return await _context.Urls.ToListAsync();
    }

    public async Task<Url?> GetById(Guid id)
    {
        return await _context.Urls.FindAsync(id);
    }

    public async Task Add(Url url)
    {
        await _context.Urls.AddAsync(url);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Url url)
    {
        _context.Entry(url).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var url = await _context.Urls.FindAsync(id);
        if (url != null)
        {
            _context.Urls.Remove(url);
            await _context.SaveChangesAsync();
        }
    }
}