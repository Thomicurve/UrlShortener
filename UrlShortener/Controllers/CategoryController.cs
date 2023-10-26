using Microsoft.AspNetCore.Mvc;

namespace UrlShortener;

[ApiController]
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
        });
        _context.SaveChanges();

        return Ok("Categoría creada exitosamente");
    }

    [HttpGet]
    public IActionResult GetAll() {
        List<CategoryForViewDto> categories = _context.Categories.Select(c => new CategoryForViewDto {
            Id = c.Id,
            Name = c.Name
        }).ToList();
        
        return Ok(categories);
    }
}
