using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class CitaRepository : ICitaRepository
{
    private readonly ApplicationDbContext _context;

    public CitaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Metodos sincronos

    public bool Insert(Cita entity)
    {
        _context.Citas.Add(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(Cita entity)
    {
        _context.Citas.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(int id)
    {
        var entity = _context.Citas.Find(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Citas.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public Cita Get(int id)
    {
        return _context.Citas.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Active)!;
    }

    public IEnumerable<Cita> GetAll()
    {
        return _context.Citas.AsNoTracking().Where(x => x.Active).ToList();
    }

    public IEnumerable<Cita> GetAllWithPagination(int page, int pageSize)
    {
        return _context.Citas.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int Count()
    {
        return _context.Citas.Count(x => x.Active);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(Cita entity)
    {
        await _context.Citas.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Cita entity)
    {
        _context.Citas.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Citas.FindAsync(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Citas.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Cita> GetAsync(int id)
    {
        return (await _context.Citas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active))!;
    }

    public async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas.AsNoTracking().Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<Cita>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        return await _context.Citas.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Citas.CountAsync(x => x.Active);
    }

    #endregion
}
