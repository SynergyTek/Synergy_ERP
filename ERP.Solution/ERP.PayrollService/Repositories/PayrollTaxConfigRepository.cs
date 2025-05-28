using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayrollTaxConfigRepository : GenericRepository<PayrollTaxConfig>, IPayrollTaxConfigRepository
    {
        public PayrollTaxConfigRepository(PayrollDbContext context) : base(context) { }
    }
} 