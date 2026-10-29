using DTO.Cita;
using Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
public class CitaController : ApiControllerBase
{
    private readonly ICitaApplication _application;

    public CitaController(ICitaApplication application)
    {
        _application = application;
    }

    [AllowAnonymous]
    [HttpPost("public-reserva")]
    public async Task<IActionResult> RegistrarReservaPublicaAsync([FromBody] PublicReservaDTO dto, CancellationToken cancellationToken)
    {
        var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        var result = await _application.RegistrarReservaPublicaAsync(dto, clientIp, cancellationToken);
        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] CitaDTO dto, CancellationToken cancellationToken)
    {
        var result = await _application.InsertAsync(dto, cancellationToken);
        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] CitaDTO dto, CancellationToken cancellationToken)
    {
        var result = await _application.UpdateAsync(dto, cancellationToken);
        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _application.DeleteAsync(id, cancellationToken);
        return result.Match(
            success => NoContent(),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _application.GetAllAsync(cancellationToken);
        return result.Match(
            items => Ok(items),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var result = await _application.GetAsync(id, cancellationToken);
        return result.Match(
            item => Ok(item),
            errors => Problem(errors));
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await _application.GetAllWithPaginationAsync(page, pageSize, cancellationToken);
        return result.Match(
            items => Ok(items),
            errors => Problem(errors));
    }

    [HttpGet("count")]
    public async Task<IActionResult> CountAsync(CancellationToken cancellationToken)
    {
        var result = await _application.CountAsync(cancellationToken);
        return result.Match(
            count => Ok(count),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpGet("token/{token:guid}")]
    public async Task<IActionResult> GetByTokenAsync(Guid token, CancellationToken cancellationToken)
    {
        var result = await _application.GetByTokenAsync(token, cancellationToken);
        return result.Match(
            item => Ok(item),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("accion-cliente")]
    public async Task<IActionResult> AccionClienteAsync([FromQuery] Guid token, [FromQuery] string accion, CancellationToken cancellationToken)
    {
        var result = await _application.AccionClienteAsync(token, accion, cancellationToken);
        return result.Match(
            success => Ok(new { success = true, mensaje = $"Cita procesada con acción: {accion}" }),
            errors => Problem(errors));
    }
}
