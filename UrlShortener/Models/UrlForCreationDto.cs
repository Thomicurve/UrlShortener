using System.ComponentModel.DataAnnotations;

namespace UrlShortener;

public class UrlForCreationDto
{
    [RegularExpression(@"^.*(?=.*\/.*\/)(?=.*:).*$", ErrorMessage = "La url debe ser válida")]
    public string Url { get; set; }
}
