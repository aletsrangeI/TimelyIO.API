using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class ServicioRepository : IServicioRepository
{
    private readonly ApplicationDbContext _context;

    public ServicioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Metodos sincronos

    public bool Insert(Servicio entity)
    {
        _context.Servicios.Add(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(Servicio entity)
    {
        _context.Servicios.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(int id)
    {
        var entity = _context.Servicios.Find(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Servicios.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public Servicio Get(int id)
    {
        return _context.Servicios.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Active)!;
    }

    public IEnumerable<Servicio> GetAll()
    {
        return _context.Servicios.AsNoTracking().Where(x => x.Active).ToList();
    }

    public IEnumerable<Servicio> GetAllWithPagination(int page, int pageSize)
    {
        return _context.Servicios.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int Count()
    {
        return _context.Servicios.Count(x => x.Active);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(Servicio entity)
    {
        await _context.Servicios.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Servicio entity)
    {
        _context.Servicios.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Servicios.FindAsync(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Servicios.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Servicio> GetAsync(int id)
    {
        return (await _context.Servicios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active))!;
    }

    public async Task<IEnumerable<Servicio>> GetAllAsync()
    {
        return await _context.Servicios.AsNoTracking().Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<Servicio>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        return await _context.Servicios.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Servicios.CountAsync(x => x.Active);
    }

    #endregion
}
