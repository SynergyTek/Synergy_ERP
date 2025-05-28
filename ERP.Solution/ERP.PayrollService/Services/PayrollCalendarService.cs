using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.PayrollService.ViewModels;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Services
{
    public class PayrollCalendarService : IPayrollCalendarService
    {
        private readonly IPayrollCalendarRepository _calendarRepo;

        public PayrollCalendarService(IPayrollCalendarRepository calendarRepo)
        {
            _calendarRepo = calendarRepo;
        }

        public async Task<IEnumerable<PayrollCalendarViewModel>> GetCalendarsAsync()
        {
            var calendars = await _calendarRepo.GetAllAsync();
            var vms = new List<PayrollCalendarViewModel>();
            foreach (var c in calendars)
            {
                vms.Add(new PayrollCalendarViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Frequency = c.Frequency,
                    StartDate = c.StartDate
                });
            }
            return vms;
        }

        public async Task<PayrollCalendarViewModel> GetCalendarByIdAsync(int id)
        {
            var c = await _calendarRepo.GetByIdAsync(id);
            if (c == null) return null;
            return new PayrollCalendarViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Frequency = c.Frequency,
                StartDate = c.StartDate
            };
        }

        public async Task<PayrollCalendarViewModel> CreateCalendarAsync(PayrollCalendarViewModel vm)
        {
            var entity = new PayrollCalendar
            {
                Name = vm.Name,
                Frequency = vm.Frequency,
                StartDate = vm.StartDate
            };
            var created = await _calendarRepo.AddAsync(entity);
            vm.Id = created.Id;
            return vm;
        }

        public async Task<PayrollCalendarViewModel> UpdateCalendarAsync(PayrollCalendarViewModel vm)
        {
            var entity = await _calendarRepo.GetByIdAsync(vm.Id);
            if (entity == null) return null;
            entity.Name = vm.Name;
            entity.Frequency = vm.Frequency;
            entity.StartDate = vm.StartDate;
            await _calendarRepo.UpdateAsync(entity);
            return vm;
        }

        public async Task<bool> DeleteCalendarAsync(int id)
        {
            return await _calendarRepo.DeleteAsync(id);
        }
    }
} 