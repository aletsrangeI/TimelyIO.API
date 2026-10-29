using Domain.Entities;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ApplicationDbContext _context;

    public ClienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Metodos sincronos

    public bool Insert(Cliente entity)
    {
        _context.Clientes.Add(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Update(Cliente entity)
    {
        _context.Clientes.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public bool Delete(int id)
    {
        var entity = _context.Clientes.Find(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Clientes.Update(entity);
        var result = _context.SaveChanges();
        return result > 0;
    }

    public Cliente Get(int id)
    {
        return _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id && x.Active)!;
    }

    public IEnumerable<Cliente> GetAll()
    {
        return _context.Clientes.AsNoTracking().Where(x => x.Active).ToList();
    }

    public IEnumerable<Cliente> GetAllWithPagination(int page, int pageSize)
    {
        return _context.Clientes.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public int Count()
    {
        return _context.Clientes.Count(x => x.Active);
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(Cliente entity)
    {
        await _context.Clientes.AddAsync(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> UpdateAsync(Cliente entity)
    {
        _context.Clientes.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Clientes.FindAsync(id);
        if (entity is null) return false;
        entity.Active = false;
        _context.Clientes.Update(entity);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Cliente> GetAsync(int id)
    {
        return (await _context.Clientes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active))!;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes.AsNoTracking().Where(x => x.Active).ToListAsync();
    }

    public async Task<IEnumerable<Cliente>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        return await _context.Clientes.AsNoTracking()
            .Where(x => x.Active)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Clientes.CountAsync(x => x.Active);
    }

    #endregion
}
