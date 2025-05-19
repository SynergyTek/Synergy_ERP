using Microsoft.EntityFrameworkCore;
using Synergy.Data.Model;

namespace Synergy.Business.Interface;

public interface IContextBase<TDm, TContext>
{
    Task<IEnumerable<TDm>?> GetAllAsync();
    Task<TDm?> GetByIdAsync(Guid id);
    Task<TDm?> AddAsync(TDm entity);
    Task<TDm?> UpdateAsync(TDm entity);
    Task<bool> DeleteAsync(Guid id);
}