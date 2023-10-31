using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace UrlShortener;

public class Url
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string LongUrl { get; set; }
    [Required]
    public string ShortUrl { get; set; }
    [Required]
    public int VisitCounter { get; set; }
    [AllowNull]
    public int? CategoryId { get; set; }
    public Category Category { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
