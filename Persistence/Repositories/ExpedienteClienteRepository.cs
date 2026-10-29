using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class ExpedienteClienteRepository : IExpedienteClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ExpedienteClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Metodos sincronos

    public bool Insert(ExpedienteCliente entity)
    {
        _context.ExpedientesClientes.Add(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(ExpedienteCliente entity)
    {
        _context.ExpedientesClientes.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(int id)
    {
        var entity = _context.ExpedientesClientes.Find(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.ExpedientesClientes.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public ExpedienteCliente Get(int id)
    {
        return _context.ExpedientesClientes.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Active)!;
    }

    public IEnumerable<ExpedienteCliente> GetAll()
    {
        return _context.ExpedientesClientes.AsNoTracking().Where(x => x.Active).ToList();
    }

    public IEnumerable<ExpedienteCliente> GetAllWithPagination(int page, int pageSize)
    {
        return _context.ExpedientesClientes.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int Count()
    {
        return _context.ExpedientesClientes.Count(x => x.Active);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(ExpedienteCliente entity)
    {
        await _context.ExpedientesClientes.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(ExpedienteCliente entity)
    {
        _context.ExpedientesClientes.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.ExpedientesClientes.FindAsync(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.ExpedientesClientes.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<ExpedienteCliente> GetAsync(int id)
    {
        return (await _context.ExpedientesClientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active))!;
    }

    public async Task<IEnumerable<ExpedienteCliente>> GetAllAsync()
    {
        return await _context.ExpedientesClientes.AsNoTracking().Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<ExpedienteCliente>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        return await _context.ExpedientesClientes.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.ExpedientesClientes.CountAsync(x => x.Active);
    }

    #endregion
}
