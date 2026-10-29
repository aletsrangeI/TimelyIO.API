using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class ProfesionalRepository : IProfesionalRepository
{
    private readonly ApplicationDbContext _context;

    public ProfesionalRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Metodos sincronos

    public bool Insert(Profesional entity)
    {
        _context.Profesionales.Add(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(Profesional entity)
    {
        _context.Profesionales.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(int id)
    {
        var entity = _context.Profesionales.Find(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Profesionales.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public Profesional Get(int id)
    {
        return _context.Profesionales.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Active)!;
    }

    public IEnumerable<Profesional> GetAll()
    {
        return _context.Profesionales.AsNoTracking().Where(x => x.Active).ToList();
    }

    public IEnumerable<Profesional> GetAllWithPagination(int page, int pageSize)
    {
        return _context.Profesionales.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int Count()
    {
        return _context.Profesionales.Count(x => x.Active);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(Profesional entity)
    {
        await _context.Profesionales.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Profesional entity)
    {
        _context.Profesionales.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Profesionales.FindAsync(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Profesionales.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Profesional> GetAsync(int id)
    {
        return (await _context.Profesionales.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active))!;
    }

    public async Task<IEnumerable<Profesional>> GetAllAsync()
    {
        return await _context.Profesionales.AsNoTracking().Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<Profesional>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        return await _context.Profesionales.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Profesionales.CountAsync(x => x.Active);
    }

    #endregion
}
