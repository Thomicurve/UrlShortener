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

    [HttpGet]
    public IActionResult GetAllUrls() {
        var urls = _context.Urls.ToList();
        return Ok(urls);
    }

    [HttpPost]
    public IActionResult CreateUrl([FromBody] UrlForCreationDto urlDto) {
        string stringRandom = UrlTransfomer.GenerarCadenaAleatoria();
        if(urlDto.CategoryId != null) {
            Category? categorySelected = _context.Categories
                .FirstOrDefault(c => c.Id == urlDto.CategoryId);

            if(categorySelected is null) {
                return NotFound("La categoría no existe");
            }
        }

        _context.Add(new Url() {
            LongUrl = urlDto.Url,
            ShortUrl = "https://urlshortx/" + stringRandom,
            VisitCounter = 0,
            CategoryId = urlDto.CategoryId
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

        url.VisitCounter++;
        _context.Urls.Update(url);
        _context.SaveChanges();

        return Redirect(url.LongUrl);
    }
}
