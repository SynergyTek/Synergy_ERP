# Payroll Service Documentation

## Overview

The Payroll Service is a .NET Core Web API that manages payroll operations, including payroll structures, payslip generation, reporting, and AI-powered chat interactions. It is modular, extensible, and integrates with AI agents for advanced chat-based automation.

---

## Architecture Diagram

```
+-------------------+         +---------------------+         +---------------------+
|                   |  HTTP   |                     |  DI     |                     |
|   Client (UI or   +-------->+  Payroll Controller +-------->+  Payroll Services   |
|   Chatbot Frontend|         |  (REST API Layer)   |         |  (Business Logic)   |
+-------------------+         +---------------------+         +---------------------+
         |                             |                                 |
         |                             v                                 v
         |                   +---------------------+         +---------------------+
         |                   | PayrollAIBotController|       |   Data Repositories |
         |                   |  (Chat/AI Endpoint)  |        |   (EF Core, DB)     |
         |                   +---------------------+         +---------------------+
         |                             |                                 |
         |                             v                                 v
         |                   +---------------------+         +---------------------+
         |                   |   IPayrollAIBotService|      |   Database (Postgres)|
         |                   +---------------------+         +---------------------+
         |                             |
         |                             v
         |                   +---------------------+
         |                   |   AutoGen Agent     |
         |                   | (optional, Python)  |
         |                   +---------------------+
```

---

## Main Components

### 1. Controllers
- **PayrollController**: Handles REST endpoints for payroll structures, payslips, reports, and calendars.
- **PayrollAIBotController**: Handles chat-based requests, routes them to the AI bot service, and (optionally) integrates with AutoGen.

### 2. Services
- **PayrollService**: Business logic for payroll structures.
- **PayslipService**: Handles payslip generation and batch processing.
- **PayrollReportService**: Reporting logic.
- **PayrollAIBotService**: AI-powered chat actions, intent recognition, and orchestration.

### 3. Interfaces
- Define contracts for services and repositories (e.g., `IPayrollAIBotService`, `IPayslipService`).

### 4. Repositories
- Data access layer using Entity Framework Core.

### 5. Models & ViewModels
- Data structures for employees, payslips, payroll structures, etc.

---

## API Endpoints

### Payroll Structure
- `GET /api/payroll/structures` — List all payroll structures
- `GET /api/payroll/structures/{id}` — Get a payroll structure by ID
- `POST /api/payroll/structures` — Create a new payroll structure
- `PUT /api/payroll/structures/{id}` — Update a payroll structure
- `DELETE /api/payroll/structures/{id}` — Delete a payroll structure

### Payslips
- `POST /api/payroll/payslips/generate` — Generate a payslip for an employee
- `POST /api/payroll/payslips/generate-batch` — Generate payslips for multiple employees
- `GET /api/payroll/payslips/employee/{employeeId}` — Get payslips for an employee
- `GET /api/payroll/payslips/{payslipId}` — Get a payslip by ID

### Reporting
- `GET /api/payroll/report/payslips?periodStart=yyyy-MM-dd&periodEnd=yyyy-MM-dd` — Payslip report for a period
- `GET /api/payroll/report/total-payroll?periodStart=yyyy-MM-dd&periodEnd=yyyy-MM-dd` — Total payroll for a period

### Payroll Calendar
- `GET /api/payroll/calendars` — List payroll calendars
- `POST /api/payroll/calendars` — Create a payroll calendar
- `PUT /api/payroll/calendars/{id}` — Update a payroll calendar
- `DELETE /api/payroll/calendars/{id}` — Delete a payroll calendar

### AI Bot (Chat)
- `POST /api/PayrollAIBot/chat` — Accepts a chat message and parameters, routes to the correct payroll action or AI response

---

## Data Flow Example: Generate Payslip via Chat

1. **User** sends a chat message:  
   `"generate payslip for employee 1, structure 2, period 2024-06-01 to 2024-06-30"`

2. **PayrollAIBotController** receives the message at `/api/PayrollAIBot/chat`.

3. **Intent Recognition**:  
   The controller detects the intent is to generate a payslip.

4. **Service Call**:  
   The controller calls `IPayrollAIBotService.GeneratePayslipAsync(employeeId, structureId, periodStart, periodEnd)`.

5. **Business Logic**:  
   The service may call `IPayslipService.GeneratePayslipAsync` to perform the actual generation.

6. **Response**:  
   The result is returned to the user as a chat message.

---

## AutoGen Integration (Optional)

- If using AutoGen, the chat message is forwarded to an AutoGen agent (Python service).
- The agent interprets the message, calls the appropriate .NET API endpoint, and returns the result.

---

## Dependency Injection

All services and repositories are registered in `Program.cs` for dependency injection, ensuring loose coupling and testability.

---

## Extending the System

- **Add new payroll actions** by updating the service interfaces and implementations.
- **Enhance AI capabilities** by integrating with LLMs or AutoGen for more advanced chat understanding.
- **Secure endpoints** with authentication/authorization as needed.

---

## Example Chat Request

```json
POST /api/PayrollAIBot/chat
{
  "message": "generate payslip",
  "employeeId": 1,
  "structureId": 2,
  "periodStart": "2024-06-01",
  "periodEnd": "2024-06-30"
}
```

---

## Summary

This Payroll Service provides a robust, extensible backend for payroll management, with both traditional REST and AI-powered chat interfaces. It is ready for integration with modern chatbots, LLMs, and automation frameworks like AutoGen.

---