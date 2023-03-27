using Microsoft.EntityFrameworkCore;
using UrlShortener.Models;

namespace Data;

public class UrlContext : DbContext
{
    public UrlContext(DbContextOptions<UrlContext>options) : base(options)
    {
        
    }

    public DbSet<Url?> Urls { get; set; }
}