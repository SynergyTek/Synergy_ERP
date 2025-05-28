# ERP.PayrollService

A standalone .NET Core Web API for Payroll management, inspired by Odoo Payroll.

## Features
- Payroll structure (rules, allowances, deductions)
- Payslip generation (batch, individual)
- Payslip self-service
- Payroll tax config/computation
- Bulk payroll processing
- Payroll-related reporting
- Payroll calendar/scheduling

## Tech Stack
- .NET Core Web API
- Entity Framework Core
- PostgreSQL

## Getting Started

### 1. Update Connection String
Edit `appsettings.json` with your PostgreSQL credentials:
```
"ConnectionStrings": {
  "PayrollDb": "Host=localhost;Port=5432;Database=erp_payroll;Username=postgres;Password=yourpassword"
}
```

### 2. Add Migration & Update Database
```
dotnet ef migrations add InitialCreate -p ERP.PayrollService/ERP.PayrollService.csproj -s ERP.PayrollService/ERP.PayrollService.csproj

dotnet ef database update -p ERP.PayrollService/ERP.PayrollService.csproj -s ERP.PayrollService/ERP.PayrollService.csproj
```

### 3. Run the API
```
dotnet run --project ERP.PayrollService/ERP.PayrollService.csproj
```

## API Endpoints
- `GET /api/payroll/structures` - List payroll structures
- `POST /api/payroll/structures` - Create payroll structure
- `PUT /api/payroll/structures/{id}` - Update payroll structure
- `DELETE /api/payroll/structures/{id}` - Delete payroll structure
- `POST /api/payroll/payslips/generate` - Generate payslip for employee
- `POST /api/payroll/payslips/generate-batch` - Generate payslips for multiple employees
- `GET /api/payroll/payslips/employee/{employeeId}` - Get payslips for employee
- `GET /api/payroll/payslips/{payslipId}` - Get payslip by ID
- `GET /api/payroll/report/payslips?periodStart=yyyy-MM-dd&periodEnd=yyyy-MM-dd` - Payslip report
- `GET /api/payroll/report/total-payroll?periodStart=yyyy-MM-dd&periodEnd=yyyy-MM-dd` - Total payroll
- `GET /api/payroll/calendars` - List payroll calendars
- `POST /api/payroll/calendars` - Create payroll calendar
- `PUT /api/payroll/calendars/{id}` - Update payroll calendar
- `DELETE /api/payroll/calendars/{id}` - Delete payroll calendar

## Dependency Injection
All repositories and services are registered for DI in `Program.cs`.

## License
MIT 