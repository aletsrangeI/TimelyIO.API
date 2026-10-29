using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers;
namespace WebApi.Controllers;

public class RefreshRequest
{
    public string RefreshToken { get; set; } = string.Empty;
}

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller
{
    private readonly AppSettings _appSettings;

    public AuthController(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    [HttpPost("Refresh")]
    public IActionResult Refresh([FromBody] RefreshRequest request)
    {
        if (string.IsNullOrEmpty(request.RefreshToken))
            return BadRequest(new { Message = "Refresh token required" });

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        try
        {
            // Validate the refresh token without checking expiration
            var principal = tokenHandler.ValidateToken(request.RefreshToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _appSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _appSettings.Audience,
                ValidateLifetime = false, // ignore expiration for refresh token
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            // Issue new access token (short‑lived) and a new refresh token
            var newAccessToken = BuildToken(principal);
            var newRefreshToken = BuildRefreshToken(principal);

            return Ok(new { Token = newAccessToken, RefreshToken = newRefreshToken });
        }
        catch
        {
            // Invalid refresh token
            return Unauthorized(new { Message = "Invalid refresh token" });
        }
    }

    private string BuildToken(ClaimsPrincipal principal)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(principal.Claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string BuildRefreshToken(ClaimsPrincipal principal)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(principal.Claims),
            Expires = DateTime.UtcNow.AddDays(7), // long‑lived refresh token
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
