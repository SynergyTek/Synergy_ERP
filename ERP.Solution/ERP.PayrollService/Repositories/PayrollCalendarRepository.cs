using ERP.PayrollService.Models;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Data;

namespace ERP.PayrollService.Repositories
{
    public class PayrollCalendarRepository : GenericRepository<PayrollCalendar>, IPayrollCalendarRepository
    {
        public PayrollCalendarRepository(PayrollDbContext context) : base(context) { }
    }
} 