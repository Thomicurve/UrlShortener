using Microsoft.AspNetCore.Mvc;

namespace UrlShortener;

[ApiController]
[Route("api/[controller]")]
public class UrlController : ControllerBase
{
    private readonly UrlShortenerContext _context;
    public UrlController(UrlShortenerContext context) {
        _context = context;
    }

    [HttpPost]
    public IActionResult CreateUrl([FromBody] UrlForCreationDto urlDto) {
        string stringRandom = UrlTransfomer.GenerarCadenaAleatoria();

        _context.Add(new Url() {
            LongUrl = urlDto.Url,
            ShortUrl = "https://urlshortx/" + stringRandom
        });
        _context.SaveChanges();

        return Ok("Url recortada exitosamente");
    }

    [HttpPost("redirect")]
    public IActionResult GetUrl([FromBody] UrlForRedirectDto urlDto) {
        var url = _context.Urls.FirstOrDefault(u => u.ShortUrl == urlDto.ShortUrl);

        if (url == null) {
            return NotFound("La url no existe");
        }

        return Redirect(url.LongUrl);
    }
}
