using DTO.ContenidoCatalogo;
using Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ContenidoCatalogoController : Controller
{
    private readonly IContenidoCatalogoApplication _contenidoCatalogoApplication;

    public ContenidoCatalogoController(IContenidoCatalogoApplication contenidoCatalogoApplication)
    {
        _contenidoCatalogoApplication = contenidoCatalogoApplication;
    }

    #region Metodos sincronos

    [HttpPost("Insert")]
    public IActionResult Insert([FromBody] ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = _contenidoCatalogoApplication.Insert(contenidoCatalogoDto);
        return Ok(response);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = _contenidoCatalogoApplication.Update(contenidoCatalogoDto);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var response = _contenidoCatalogoApplication.Delete(id);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var response = _contenidoCatalogoApplication.getAll();
        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var response = _contenidoCatalogoApplication.Get(id);
        return Ok(response);
    }

    [HttpGet("GetallWithPagination")]
    public IActionResult GetallWithPagination(int page, int pageSize)
    {
        var response = _contenidoCatalogoApplication.GetAllWithPagination(page, pageSize);
        return Ok(response);
    }

    [HttpGet("Count")]
    public IActionResult Count()
    {
        var response = _contenidoCatalogoApplication.Count();
        return Ok(response);
    }

    [HttpGet("GetContenidoCatalogoByCatalogoId/{id}")]
    public IActionResult GetContenidoCatalogoByCatalogoId(int id)
    {
        var response = _contenidoCatalogoApplication.GetContenidoCatalogoByCatalogoId(id);
        return Ok(response);
    }

    #endregion

    #region metodos asincronos

    [HttpPost("InsertAsync")]
    public async Task<IActionResult> InsertAsync([FromBody] ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = await _contenidoCatalogoApplication.InsertAsync(contenidoCatalogoDto);
        return Ok(response);
    }

    [HttpPut("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync([FromBody] ContenidoCatalogoDTO contenidoCatalogoDto)
    {
        var response = await _contenidoCatalogoApplication.UpdateAsync(contenidoCatalogoDto);
        return Ok(response);
    }

    [HttpDelete("DeleteAsync")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _contenidoCatalogoApplication.DeleteAsync(id);
        return Ok(response);
    }

    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _contenidoCatalogoApplication.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("GetAsync/{id}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var response = await _contenidoCatalogoApplication.GetAsync(id);
        return Ok(response);
    }

    [HttpGet("GetAllWithPaginationAsync")]
    public async Task<IActionResult> GetAllWithPaginationAsync(int page, int pageSize)
    {
        var response = await _contenidoCatalogoApplication.GetAllWithPaginationAsync(page, pageSize);
        return Ok(response);
    }

    [HttpGet("CountAsync")]
    public async Task<IActionResult> CountAsync()
    {
        var response = await _contenidoCatalogoApplication.CountAsync();
        return Ok(response);
    }

    [HttpGet("GetContenidoCatalogoByCatalogoIdAsync/{id}")]
    public async Task<IActionResult> GetContenidoCatalogoByCatalogoIdAsync(int id)
    {
        var response = await _contenidoCatalogoApplication.GetContenidoCatalogoByCatalogoIdAsync(id);
        return Ok(response);
    }
    
    #endregion
}