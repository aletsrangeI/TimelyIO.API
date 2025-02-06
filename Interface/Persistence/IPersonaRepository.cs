using Domain.Entities;
using DTO.Persona;

namespace Interface.Persistence;

public interface IPersonaRepository : IGenericRepository<Persona>
{
    Persona Authenticate(PersonaLoginDTO personaLogin);
    
    Task<Persona> AuthenticateAsync(PersonaLoginDTO personaLogin);
}