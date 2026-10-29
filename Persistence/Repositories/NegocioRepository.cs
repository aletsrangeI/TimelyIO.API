using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class NegocioRepository : INegocioRepository
{
    private readonly ApplicationDbContext _context;

    public NegocioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Metodos sincronos

    public bool Insert(Negocio entity)
    {
        _context.Negocios.Add(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(Negocio entity)
    {
        _context.Negocios.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(int id)
    {
        var entity = _context.Negocios.Find(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Negocios.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public Negocio Get(int id)
    {
        return _context.Negocios.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Active)!;
    }

    public IEnumerable<Negocio> GetAll()
    {
        return _context.Negocios.AsNoTracking().Where(x => x.Active).ToList();
    }

    public IEnumerable<Negocio> GetAllWithPagination(int page, int pageSize)
    {
        return _context.Negocios.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int Count()
    {
        return _context.Negocios.Count(x => x.Active);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(Negocio entity)
    {
        await _context.Negocios.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Negocio entity)
    {
        _context.Negocios.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Negocios.FindAsync(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Negocios.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Negocio> GetAsync(int id)
    {
        return (await _context.Negocios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active))!;
    }

    public async Task<IEnumerable<Negocio>> GetAllAsync()
    {
        return await _context.Negocios.AsNoTracking().Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<Negocio>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        return await _context.Negocios.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Negocios.CountAsync(x => x.Active);
    }

    #endregion
}
