using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class DeductionRepository : GenericRepository<Deduction>, IDeductionRepository
    {
        public DeductionRepository(PayrollDbContext context) : base(context) { }
    }
} 