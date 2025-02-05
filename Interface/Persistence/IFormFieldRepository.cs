using Domain.Entities;

namespace Interface.Persistence;

public interface IFormFieldRepository : IGenericRepository<FormField>
{
    public IEnumerable<FormField> GetFormFieldByFormCatId(int id);
    public Task<IEnumerable<FormField>> GetFormFieldByFormCatIdAsync(int id);
}