using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP.HRService.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly HRDbContext _context;
        public ContractRepository(HRDbContext context) { _context = context; }

        public async Task<Contract?> GetByIdAsync(string id) =>
            await _context.Contracts
                .Include(c => c.Employee)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task<IEnumerable<Contract>> GetAllAsync() =>
            await _context.Contracts
                .Include(c => c.Employee)
                .ToListAsync();

        public async Task AddAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _context.Contracts.FindAsync(id);
            if (entity != null)
            {
                _context.Contracts.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
} 