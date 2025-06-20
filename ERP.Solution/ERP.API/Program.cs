using ERP.HRService.Data;
using ERP.HRService.Interfaces;
using ERP.HRService.Repositories;
using ERP.HRService.Services;
using ERP.PayrollService.Data;
using ERP.PayrollService.Interfaces;
using ERP.PayrollService.Repositories;
using ERP.PayrollService.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Add DbContext
var payrollConnection = builder.Configuration.GetConnectionString("PayrollConnection");
var hrConnection = builder.Configuration.GetConnectionString("HrConnection");

//// Register MainDbContext
//builder.Services.AddDbContext<PayrollDbContext>(options =>
//    options.UseNpgsql(payrollConnection));

// Register HRDbContext
builder.Services.AddDbContext<HRDbContext>(options =>
    options.UseNpgsql(hrConnection));

//HR Service
builder.Services.AddScoped<ERP.HRService.Interfaces.IEmployeeRepository, ERP.HRService.Repositories.EmployeeRepository>();
builder.Services.AddScoped<ERP.HRService.Interfaces.IEmployeeService, ERP.HRService.Services.EmployeeService>();
builder.Services.AddScoped<IDepartmentRepository, ERP.HRService.Repositories.DepartmentRepository>();
builder.Services.AddScoped<ERP.HRService.Interfaces.IDepartmentService, ERP.HRService.Services.DepartmentService > ();
builder.Services.AddScoped<ERP.HRService.Interfaces.IJobRepository, ERP.HRService.Repositories.JobRepository>();
builder.Services.AddScoped<ERP.HRService.Interfaces.IJobService, ERP.HRService.Services.JobService >();
builder.Services.AddScoped<ERP.HRService.Interfaces.IContractRepository, ERP.HRService.Repositories.ContractRepository>();
//builder.Services.AddScoped<ERP.HRService.Interfaces.IContractService, ERP.HRService.Services.ContractService >();
builder.Services.AddScoped<ERP.HRService.Interfaces.IAttendanceRepository, ERP.HRService.Repositories.AttendanceRepository>();
builder.Services.AddScoped<ERP.HRService.Interfaces.IAttendanceService, ERP.HRService.Services.AttendanceService >();
builder.Services.AddScoped<ERP.HRService.Interfaces.ILeaveRepository, ERP.HRService.Repositories.LeaveRepository>();
//builder.Services.AddScoped<ERP.HRService.Interfaces.ILeaveService, ERP.HRService.Services.LeaveService >();
builder.Services.AddScoped<ERP.HRService.Interfaces.ISkillRepository, ERP.HRService.Repositories.SkillRepository>();
builder.Services.AddScoped<ERP.HRService.Interfaces.ISkillService, ERP.HRService.Services.SkillService >();

// Register repositories
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped<IPayrollStructureRepository, PayrollStructureRepository>();
//builder.Services.AddScoped<IPayrollRuleRepository, PayrollRuleRepository>();
//builder.Services.AddScoped<IAllowanceRepository, AllowanceRepository>();
//builder.Services.AddScoped<IDeductionRepository, DeductionRepository>();
//builder.Services.AddScoped<IPayslipRepository, PayslipRepository>();
//builder.Services.AddScoped<IPayrollBatchRepository, PayrollBatchRepository>();
//builder.Services.AddScoped<IPayrollCalendarRepository, PayrollCalendarRepository>();
//builder.Services.AddScoped<IPayrollTaxConfigRepository, PayrollTaxConfigRepository>();
////builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
////builder.Services.AddScoped<ILeaveRepository, LeaveRepository>();
//builder.Services.AddScoped<ILoanRepository, LoanRepository>();
//builder.Services.AddScoped<ILoanRepository, LoanRepository>();
//builder.Services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
//builder.Services.AddScoped<IPayslipAdjustmentRepository, PayslipAdjustmentRepository>();
//builder.Services.AddScoped<IPayslipStatusChangeLogRepository, PayslipStatusChangeLogRepository>();


//// Register services
//builder.Services.AddScoped<IPayrollService, PayrollService>();
//builder.Services.AddScoped<IPayslipService, PayslipService>();
//builder.Services.AddScoped<IPayrollReportService, PayrollReportService>();
//builder.Services.AddScoped<IPayrollCalendarService, PayrollCalendarService>();
//builder.Services.AddScoped<IPayrollTaxConfigService, PayrollTaxConfigService>();
////builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//builder.Services.AddScoped<IPayrollRuleService, PayrollRuleService>();
//builder.Services.AddScoped<IAllowanceService, AllowanceService>();
//builder.Services.AddScoped<IDeductionService, DeductionService>();

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
