using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayrollBatchRepository : GenericRepository<PayrollBatch>, IPayrollBatchRepository
    {
        public PayrollBatchRepository(PayrollDbContext context) : base(context) { }
    }
} 