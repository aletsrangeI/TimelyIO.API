using DTO;
using Interface.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class CatalogoController : Controller
{
    private readonly ICatalogoApplication _catalogoApplication;

    public CatalogoController(ICatalogoApplication catalogoApplication)
    {
        _catalogoApplication = catalogoApplication;
    }

    #region Metodos sincronos

    [HttpPost("Insert")]
    public IActionResult Insert([FromBody] CatalogoDTO catalogoDto)
    {
        var response = _catalogoApplication.Insert(catalogoDto);
        return Ok(response);
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] CatalogoDTO catalogoDto)
    {
        var response = _catalogoApplication.Update(catalogoDto);
        return Ok(response);
    }

    [HttpDelete("Delete")]
    public IActionResult Delete(int id)
    {
        var response = _catalogoApplication.Delete(id);
        return Ok(response);
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var response = _catalogoApplication.GetAll();
        return Ok(response);
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById(int id)
    {
        var response = _catalogoApplication.Get(id);
        return Ok(response);
    }

    [HttpGet("GetallWithPagination")]
    public IActionResult GetallWithPagination(int page, int pageSize)
    {
        var response = _catalogoApplication.GetAllWithPagination(page, pageSize);
        return Ok(response);
    }

    [HttpGet("Count")]
    public IActionResult Count()
    {
        var response = _catalogoApplication.Count();
        return Ok(response);
    }

    #endregion

    #region metodos asincronos

    [HttpPost("InsertAsync")]
    public async Task<IActionResult> InsertAsync([FromBody] CatalogoDTO catalogoDto)
    {
        var response = await _catalogoApplication.InsertAsync(catalogoDto);
        return Ok(response);
    }

    [HttpPut("UpdateAsync")]
    public async Task<IActionResult> UpdateAsync([FromBody] CatalogoDTO catalogoDto)
    {
        var response = await _catalogoApplication.UpdateAsync(catalogoDto);
        return Ok(response);
    }

    [HttpDelete("DeleteAsync")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var response = await _catalogoApplication.DeleteAsync(id);
        return Ok(response);
    }

    [HttpGet("GetAllAsync")]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _catalogoApplication.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("GetAllWithPaginationAsync")]
    public async Task<IActionResult> GetAllWithPaginationAsync(int page, int pageSize)
    {
        var response = await _catalogoApplication.GetAllWithPaginationAsync(page, pageSize);
        return Ok(response);
    }

    [HttpGet("CountAsync")]
    public async Task<IActionResult> CountAsync()
    {
        var response = await _catalogoApplication.CountAsync();
        return Ok(response);
    }

    #endregion
}