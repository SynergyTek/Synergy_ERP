using ERP.PayrollService.Data;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Repositories;
using ERP.PayrollService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<PayrollDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PayrollDb")));

// Register repositories
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPayrollStructureRepository, PayrollStructureRepository>();
builder.Services.AddScoped<IPayrollRuleRepository, PayrollRuleRepository>();
builder.Services.AddScoped<IAllowanceRepository, AllowanceRepository>();
builder.Services.AddScoped<IDeductionRepository, DeductionRepository>();
builder.Services.AddScoped<IPayslipRepository, PayslipRepository>();
builder.Services.AddScoped<IPayrollBatchRepository, PayrollBatchRepository>();
builder.Services.AddScoped<IPayrollCalendarRepository, PayrollCalendarRepository>();
builder.Services.AddScoped<IPayrollTaxConfigRepository, PayrollTaxConfigRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
builder.Services.AddScoped<IPayslipAdjustmentRepository, PayslipAdjustmentRepository>();
builder.Services.AddScoped<IPayslipStatusChangeLogRepository, PayslipStatusChangeLogRepository>();

// Register services
builder.Services.AddScoped<IPayrollService, PayrollService>();
builder.Services.AddScoped<IPayslipService, PayslipService>();
builder.Services.AddScoped<IPayrollReportService, PayrollReportService>();
builder.Services.AddScoped<IPayrollCalendarService, PayrollCalendarService>();
builder.Services.AddScoped<IPayrollTaxConfigService, PayrollTaxConfigService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPayrollRuleService, PayrollRuleService>();
builder.Services.AddScoped<IAllowanceService, AllowanceService>();
builder.Services.AddScoped<IDeductionService, DeductionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
