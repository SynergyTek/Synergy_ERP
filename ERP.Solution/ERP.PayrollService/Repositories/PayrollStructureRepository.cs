using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayrollStructureRepository : GenericRepository<PayrollStructure>, IPayrollStructureRepository
    {
        public PayrollStructureRepository(PayrollDbContext context) : base(context) { }
    }
} 