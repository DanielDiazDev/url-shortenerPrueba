using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models;

public class Url
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(2048)]
    public string OriginalUrl { get; set; }

    [Required]
    [MaxLength(100)]
    public string ShortenedUrl { get; set; }
}