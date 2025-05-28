using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class AllowanceRepository : GenericRepository<Allowance>, IAllowanceRepository
    {
        public AllowanceRepository(PayrollDbContext context) : base(context) { }
    }
} 