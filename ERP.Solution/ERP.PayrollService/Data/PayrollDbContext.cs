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
        }
    }
} 