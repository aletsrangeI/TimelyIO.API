using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class ContenidoCatalogoRepository : IContenidoCatalogoRepository
{
    protected readonly ApplicationDbContext _dbContext;

    public ContenidoCatalogoRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Metodos sincronos

    public bool Insert(ContenidoCatalogo entity)
    {
        _dbContext.ContenidoCatalogos.Add(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public bool Update(ContenidoCatalogo entity)
    {
        _dbContext.ContenidoCatalogos.Update(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        var contenidoCatalogo = Get(id);
        if (contenidoCatalogo == null) return false;
        _dbContext.ContenidoCatalogos.Remove(contenidoCatalogo);
        return _dbContext.SaveChanges() > 0;
    }

    public ContenidoCatalogo Get(int id)
    {
        return _dbContext.ContenidoCatalogos.Find(id);
    }

    public IEnumerable<ContenidoCatalogo> GetAll()
    {
        return _dbContext.ContenidoCatalogos;
    }

    public IEnumerable<ContenidoCatalogo> GetAllWithPagination(int page, int pageSize)
    {
        IEnumerable<ContenidoCatalogo> contenidoCatalogos =
            _dbContext.ContenidoCatalogos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return contenidoCatalogos;
    }

    public int Count()
    {
        return _dbContext.ContenidoCatalogos.Count();
    }

    public IEnumerable<ContenidoCatalogo> GetContenidoCatalogoByCatalogoId(int id)
    {
        return _dbContext.ContenidoCatalogos.Where(x => x.CatalogoId == id);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(ContenidoCatalogo entity)
    {
        await _dbContext.ContenidoCatalogos.AddAsync(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ContenidoCatalogo entity)
    {
        _dbContext.ContenidoCatalogos.Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var contenidoCatalogo = await GetAsync(id);
        if (contenidoCatalogo == null) return false;
        _dbContext.ContenidoCatalogos.Remove(contenidoCatalogo);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<ContenidoCatalogo> GetAsync(int id)
    {
        return await _dbContext.ContenidoCatalogos.FindAsync(id);
    }

    public async Task<IEnumerable<ContenidoCatalogo>> GetAllAsync()
    {
        return await _dbContext.ContenidoCatalogos.ToListAsync();
    }

    public async Task<IEnumerable<ContenidoCatalogo>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        IEnumerable<ContenidoCatalogo> contenidoCatalogos =
            await _dbContext.ContenidoCatalogos.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return contenidoCatalogos;
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.ContenidoCatalogos.CountAsync();
    }

    public async Task<IEnumerable<ContenidoCatalogo>> GetContenidoCatalogoByCatalogoIdAsync(int id)
    {
        return await _dbContext.ContenidoCatalogos.Where(x => x.CatalogoId == id).ToListAsync();
    }

    #endregion
}