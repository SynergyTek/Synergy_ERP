using Microsoft.EntityFrameworkCore;
using ERP.PayrollService.Models;

namespace ERP.PayrollService.Data
{
    public class PayrollDbContext : DbContext
    {
        public PayrollDbContext(DbContextOptions<PayrollDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayrollStructure> PayrollStructures { get; set; }
        public DbSet<PayrollRule> PayrollRules { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Payslip> Payslips { get; set; }
        public DbSet<PayslipLine> PayslipLines { get; set; }
        public DbSet<PayrollBatch> PayrollBatches { get; set; }
        public DbSet<PayrollCalendar> PayrollCalendars { get; set; }
        public DbSet<PayrollTaxConfig> PayrollTaxConfigs { get; set; }
        public DbSet<EmployeeContract> EmployeeContracts { get; set; }
        public DbSet<WorkedDayInput> WorkedDayInputs { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }
        public DbSet<PayslipAdjustment> PayslipAdjustments { get; set; }
        public DbSet<LocalizationInfo> LocalizationInfos { get; set; }
        public DbSet<RetroactiveChangeLog> RetroactiveChangeLogs { get; set; }
        public DbSet<PayslipStatusChangeLog> PayslipStatusChangeLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PayrollStructure
            modelBuilder.Entity<PayrollStructure>()
                .HasMany(p => p.Rules)
                .WithOne(r => r.PayrollStructure)
                .HasForeignKey(r => r.PayrollStructureId);

            modelBuilder.Entity<PayrollStructure>()
                .HasMany(p => p.Allowances)
                .WithOne(a => a.PayrollStructure)
                .HasForeignKey(a => a.PayrollStructureId);

            modelBuilder.Entity<PayrollStructure>()
                .HasMany(p => p.Deductions)
                .WithOne(d => d.PayrollStructure)
                .HasForeignKey(d => d.PayrollStructureId);

            // Payslip
            modelBuilder.Entity<Payslip>()
                .HasMany(p => p.PayslipLines)
                .WithOne(l => l.Payslip)
                .HasForeignKey(l => l.PayslipId);

            modelBuilder.Entity<Payslip>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.EmployeeId);

            modelBuilder.Entity<Payslip>()
                .HasOne(p => p.PayrollStructure)
                .WithMany()
                .HasForeignKey(p => p.PayrollStructureId);

            modelBuilder.Entity<Payslip>()
                .HasOne(p => p.PayrollBatch)
                .WithMany(b => b.Payslips)
                .HasForeignKey(p => p.PayrollBatchId);

            // PayrollBatch
            modelBuilder.Entity<PayrollBatch>()
                .HasOne(b => b.PayrollCalendar)
                .WithMany(c => c.PayrollBatches)
                .HasForeignKey(b => b.PayrollCalendarId);

            // EmployeeContract
            modelBuilder.Entity<EmployeeContract>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeContracts)
                .HasForeignKey(ec => ec.EmployeeId);

            // WorkedDayInput
            modelBuilder.Entity<WorkedDayInput>()
                .HasOne(wd => wd.Payslip)
                .WithMany(p => p.WorkedDayInputs)
                .HasForeignKey(wd => wd.PayslipId);

            // Leave
            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Employee)
                .WithMany()
                .HasForeignKey(l => l.EmployeeId);

            // Loan
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Employee)
                .WithMany()
                .HasForeignKey(l => l.EmployeeId);

            // PayslipAdjustment
            modelBuilder.Entity<PayslipAdjustment>()
                .HasOne(pa => pa.Payslip)
                .WithMany(p => p.PayslipAdjustments)
                .HasForeignKey(pa => pa.PayslipId);
        }
    }
} 