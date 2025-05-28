using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayrollRuleRepository : GenericRepository<PayrollRule>, IPayrollRuleRepository
    {
        public PayrollRuleRepository(PayrollDbContext context) : base(context) { }
    }
} 