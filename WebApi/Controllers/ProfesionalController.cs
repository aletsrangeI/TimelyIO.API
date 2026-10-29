using DTO.Profesional;
using Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.Common;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
public class ProfesionalController : ApiControllerBase
{
    private readonly IProfesionalApplication _application;

    public ProfesionalController(IProfesionalApplication application)
    {
        _application = application;
    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] ProfesionalDTO dto, CancellationToken cancellationToken)
    {
        var result = await _application.InsertAsync(dto, cancellationToken);
        return result.Match(
            success => Ok(success),
            errors => Problem(errors));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] ProfesionalDTO dto, CancellationToken cancellationToken)
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _application.GetAllAsync(cancellationToken);
        return result.Match(
            items => Ok(items),
            errors => Problem(errors));
    }

    [AllowAnonymous]
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
}
