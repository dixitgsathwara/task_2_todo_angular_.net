using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AuthController> _logger;
    private readonly IConfiguration _configuration;

    public AuthController(ApplicationDbContext context, ILogger<AuthController> logger, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _logger.LogInformation("Attempting to find user: {Username}", request.Username);
            var user = await _context.users
                .FirstOrDefaultAsync(u => u.username == request.Username && u.password == request.Password); // Remember to hash and compare passwords

            if (user == null)
            {
                _logger.LogWarning("Invalid username or password for user: {Username}", request.Username);
                return Unauthorized("Invalid username or password");
            }

            var token = GenerateJwtToken(user.username,"admin",user.id.ToString());

            _logger.LogInformation("User {Username} successfully logged in", request.Username);
            return Ok(new { result = 1, message = "Login successful", token, username=user.username });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during login");
            return StatusCode(500, "Internal server error");
        }
    }
    // private string GenerateJwtToken(User user)

    private string GenerateJwtToken(string name, string role,string id)
    {
        // var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // var claims = new[]
        // {
        //     new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
        //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //     new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.Integer64),
        //     new Claim("Id", user.id.ToString()),
        //     new Claim("Username", user.username),
        //     new Claim(ClaimTypes.Role, "admin") 
        // };

        // var token = new JwtSecurityToken(
        //     issuer: _configuration["Jwt:Issuer"],
        //     audience: _configuration["Jwt:Audience"],
        //     claims: claims,
        //     expires: DateTime.UtcNow.AddDays(1),
        //     signingCredentials: credentials
        // );

        // return new JwtSecurityTokenHandler().WriteToken(token);
        var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim("Id", id)
            });
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
