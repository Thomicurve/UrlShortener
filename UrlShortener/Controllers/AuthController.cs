using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace UrlShortener;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UrlShortenerContext _context;
    private readonly IConfiguration _config;
    public AuthController(UrlShortenerContext context, IConfiguration config) {
        _context = context;
        _config = config;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto registerDto) {
        if(registerDto.Password != registerDto.PasswordConfirmation) 
            return BadRequest("Passwords do not match");

        User? userIsRegister = _context.Users.FirstOrDefault(u => u.Username == registerDto.Username);

        if(userIsRegister != null) return BadRequest("Username already exists");
        

        var user = new User {
            Username = registerDto.Username,
            Password = registerDto.Password
        };

        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok("User created successfully");
    }

    [HttpPost("login")]
    public IActionResult Login(AuthDto authDto) {
        User? user = _context.Users.FirstOrDefault(
            u => u.Username == authDto.Username 
            && u.Password == authDto.Password);

        if(user == null) return Forbid("Invalid username or password");

        string token = this.Authenticate(user);
        return Ok(new {accessToken = token});
    }

    private string Authenticate(User user) {
        var claims = new List<Claim>
        {
            new Claim("userId", user.Id.ToString()),
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(720),
            signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        return jwt;
    }
}
