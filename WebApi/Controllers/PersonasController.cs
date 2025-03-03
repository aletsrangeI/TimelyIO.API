using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Common;
using DTO.Persona;
using Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class PersonasController : Controller
{
    private readonly AppSettings _appSettings;
    private readonly IPersonaApplication _personasApplication;

    public PersonasController(IPersonaApplication personasApplication, IOptions<AppSettings> appSettings)
    {
        _personasApplication = personasApplication;
        _appSettings = appSettings.Value;
    }

    #region Metodos sincronos

    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public IActionResult Authenticate([FromBody] PersonaLoginDTO personaLogin)
    {
        var response = _personasApplication.Authenticate(personaLogin);

        if (!response.isSuccess) return BadRequest(response);

        if (response.Data == null) return NotFound(response.Message);

        response.Data.Token = BuildToken(response);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    [ActionName("Register")]
    public IActionResult Register([FromBody] PersonaDTO persona)
    {
        var response = _personasApplication.Insert(persona);

        if (!response.isSuccess) return BadRequest(response);

        if (response.Data == null) return NotFound(response.Message);

        return Ok(response);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var response = _personasApplication.GetAll();
        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var response = _personasApplication.Get(id);
        return Ok(response);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] PersonaDTO persona)
    {
        var response = _personasApplication.Update(persona);
        return Ok(response);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        var response = _personasApplication.Delete(id);
        return Ok(response);
    }

    [HttpGet("GetallWithPagination")]
    public IActionResult GetallWithPagination(int page, int pageSize)
    {
        var response = _personasApplication.GetAllWithPagination(page, pageSize);
        return Ok(response);
    }

    [HttpGet("Count")]
    public IActionResult Count()
    {
        var response = _personasApplication.Count();
        return Ok(response);
    }

    #endregion
    
    #region Metodos asincronos
    
    [AllowAnonymous]
    [HttpPost("AuthenticateAsync")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] PersonaLoginDTO personaLogin)
    {
        var response = await _personasApplication.AuthenticateAsync(personaLogin);

        if (!response.isSuccess) return BadRequest(response);

        if (response.Data == null) return NotFound(response.Message);

        response.Data.Token = BuildToken(response);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("RegisterAsync")]
    [ActionName("RegisterAsync")]
    public async Task<IActionResult> RegisterAsync([FromBody] PersonaDTO persona)
    {
        var response = await _personasApplication.InsertAsync(persona);

        if (!response.isSuccess) return BadRequest(response);

        if (response.Data == null) return NotFound(response.Message);

        return Ok(response);
    }
    
    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _personasApplication.GetAllAsync();
        return Ok(response);
    }
    
    [HttpGet("GetByIdAsync/{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var response = await _personasApplication.GetAsync(id);
        return Ok(response);
    }
    
    [HttpPut("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync([FromBody] PersonaDTO persona)
    {
        var response = await _personasApplication.UpdateAsync(persona);
        return Ok(response);
    }
    
    [HttpDelete("DeleteAsync")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _personasApplication.DeleteAsync(id);
        return Ok(response);
    }
    
    [HttpGet("GetallWithPaginationAsync")]
    public async Task<IActionResult> GetallWithPaginationAsync(int page, int pageSize)
    {
        var response = await _personasApplication.GetAllWithPaginationAsync(page, pageSize);
        return Ok(response);
    }
    
    [HttpGet("CountAsync")]
    public async Task<IActionResult> CountAsync()
    {
        var response = await _personasApplication.CountAsync();
        return Ok(response);
    }
    
    #endregion

    private string BuildToken(Response<PersonaDTO> personaDTO)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, personaDTO.Data.Id.ToString())
            }),

            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
}