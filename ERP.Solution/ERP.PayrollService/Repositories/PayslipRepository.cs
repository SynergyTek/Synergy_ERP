using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayslipRepository : GenericRepository<Payslip>, IPayslipRepository
    {
        public PayslipRepository(PayrollDbContext context) : base(context) { }
    }
} 