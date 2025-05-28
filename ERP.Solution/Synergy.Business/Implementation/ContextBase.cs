using Microsoft.EntityFrameworkCore;
using Synergy.Business.Interface;
using Synergy.Data.Model;

namespace Synergy.Business.Implementation;

public class ContextBase<TEntity, TContext> : IContextBase<TEntity, TContext>
    where TContext : DbContext
    where TEntity : BaseModel
{
    private readonly TContext _context;

    public ContextBase(TContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync()
    {
        _context.Set<TEntity>();
        return _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }
    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}