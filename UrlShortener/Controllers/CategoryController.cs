using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly UrlShortenerContext _context;
    public CategoryController(UrlShortenerContext context) {
        _context = context;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CategoryForCreationDto categoryDto) {
        _context.Categories.Add(new Category() {
            Name = categoryDto.Name,
            UserId = int.Parse(User.Claims.First(x => x.Type == "userId").Value)
        });
        _context.SaveChanges();

        return Ok("Categoría creada exitosamente");
    }

    [HttpGet]
    public IActionResult GetAll() {
        int userId = int.Parse(User.Claims.First(x => x.Type == "userId").Value);
        List<CategoryForViewDto> categories = _context.Categories
            .Where(c => c.UserId == userId)
            .Select(c => new CategoryForViewDto {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
        
        return Ok(categories);
    }
}
