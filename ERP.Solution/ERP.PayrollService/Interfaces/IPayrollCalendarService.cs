using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using System.Collections.Generic;

namespace ERP.PayrollService.Interfaces
{
    public interface IPayrollCalendarService
    {
        Task<IEnumerable<PayrollCalendarViewModel>> GetCalendarsAsync();
        Task<PayrollCalendarViewModel> GetCalendarByIdAsync(int id);
        Task<PayrollCalendarViewModel> CreateCalendarAsync(PayrollCalendarViewModel dto);
        Task<PayrollCalendarViewModel> UpdateCalendarAsync(PayrollCalendarViewModel dto);
        Task<bool> DeleteCalendarAsync(int id);
    }
} 