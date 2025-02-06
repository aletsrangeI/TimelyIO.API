using System.Security.Cryptography;
using System.Text;
using Domain.Entities;
using DTO.Persona;
using Interface.Persistence;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class PersonaRepository : IPersonaRepository
{
    protected readonly ApplicationDbContext _dbContext;

    public PersonaRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region Hash methods

    public string Hash(string password)
    {
        var salt = GenerateSalt();
        var hashedPassword = HashWithSalt(password, salt);
        return $"{Convert.ToBase64String(salt)}:{hashedPassword}";
    }

    public bool Check(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        var salt = Convert.FromBase64String(parts[0]);
        var hash = parts[1];
        var hashedPassword = HashWithSalt(password, salt);
        return hashedPassword == hash;
    }

    private byte[] GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }

    private string HashWithSalt(string password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedPassword = new byte[salt.Length + passwordBytes.Length];

            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

            return Convert.ToBase64String(sha256.ComputeHash(saltedPassword));
        }
    }

    #endregion

    #region Metodos sincronos

    public bool Insert(Persona entity)
    {
        entity.Password = Hash(entity.Password);
        _dbContext.Personas.Add(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public bool Update(Persona entity)
    {
        entity.Password = Hash(entity.Password);
        _dbContext.Personas.Update(entity);
        return _dbContext.SaveChanges() > 0;
    }

    public bool Delete(int id)
    {
        Persona persona = Get(id);
        if (persona == null) return false;
        _dbContext.Personas.Remove(persona);
        return _dbContext.SaveChanges() > 0;
    }

    public Persona Get(int id)
    {
        return _dbContext.Personas.Find(id);
    }

    public IEnumerable<Persona> GetAll()
    {
        return _dbContext.Personas;
    }

    public IEnumerable<Persona> GetAllWithPagination(int page, int pageSize)
    {
        IEnumerable<Persona> personas =
            _dbContext.Personas.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        return personas;
    }

    public int Count()
    {
        return _dbContext.Personas.Count();
    }

    public Persona Authenticate(PersonaLoginDTO personaLogin)
    {
        var persona = _dbContext.Personas.FirstOrDefault(p => p.Email == personaLogin.Email);
        if (persona == null) return null;
        return Check(personaLogin.Password, persona.Password) ? persona : null;
    }

    #endregion

    #region Metodos asincronos

    public async Task<bool> InsertAsync(Persona entity)
    {
        entity.Password = Hash(entity.Password);
        await _dbContext.Personas.AddAsync(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(Persona entity)
    {
        entity.Password = Hash(entity.Password);
        _dbContext.Personas.Update(entity);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Persona persona = await GetAsync(id);
        if (persona == null) return false;
        _dbContext.Personas.Remove(persona);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Persona> GetAsync(int id)
    {
        return await _dbContext.Personas.FindAsync(id);
    }

    public async Task<IEnumerable<Persona>> GetAllAsync()
    {
        return await _dbContext.Personas.ToListAsync();
    }

    public async Task<IEnumerable<Persona>> GetAllWithPaginationAsync(int page, int pageSize)
    {
        IEnumerable<Persona> personas =
            await _dbContext.Personas.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return personas;
    }

    public async Task<int> CountAsync()
    {
        return await _dbContext.Personas.CountAsync();
    }

    public async Task<Persona> AuthenticateAsync(PersonaLoginDTO personaLogin)
    {
        var persona = await _dbContext.Personas.FirstOrDefaultAsync(p => p.Email == personaLogin.Email);
        if (persona == null) return null;
        return Check(personaLogin.Password, persona.Password) ? persona : null;
    }

    #endregion
}