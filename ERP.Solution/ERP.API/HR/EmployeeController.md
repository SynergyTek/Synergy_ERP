# EmployeeController API Documentation

Base route: `/api/v1/employee`

---

## 1. List Employees

**GET** `/api/v1/employee/list`

**Description:**  
Retrieve a filtered, paginated list of employees. Also returns all departments and jobs for reference.

**Query Parameters:**
- `filter` (string, optional): JSON string for advanced filtering (see example).
- `fields` (string, optional): Comma-separated list of fields to include in the response.
- `page` (int, optional): Page number (default: 1).
- `pageSize` (int, optional): Number of results per page (default: 20).

**Example Request:**
```
GET /api/v1/employee/list?filter={"and":[{"department":{"eq":"Finance"}},{"status":{"eq":"active"}}]}&fields=id,name,joiningDate&page=1&pageSize=2
```

**Example Response:**
```json
{
  "data": [
    { "id": "emp_123", "name": "Alice Johnson", "joiningDate": "2021-05-01" },
    { "id": "emp_456", "name": "Bob Smith", "joiningDate": "2022-03-15" }
  ],
  "departments": [
    { "id": "dept_1", "name": "Finance" },
    { "id": "dept_2", "name": "HR" }
  ],
  "jobs": [
    { "id": "job_1", "name": "Accountant", "description": "Handles accounts" },
    { "id": "job_2", "name": "Engineer", "description": "Engineering role" }
  ]
}
```

---

## 2. Query Employees (Advanced)

**GET** `/api/v1/employee/query`

**Description:**  
Perform advanced queries with AND/OR filters, sorting, and pagination.

**Query Parameters:**
- `filter` (string, optional): JSON string for advanced filtering.
- `sort` (string, optional): JSON string for sorting, e.g. `{"field":"name","order":"asc"}`.
- `pagination` (string, optional): JSON string for pagination, e.g. `{"page":1,"pageSize":20}`.
- `fields` (string, optional): Comma-separated list of fields to include in the response.

**Example Request:**
```
GET /api/v1/employee/query?filter={"and":[{"department":{"eq":"Finance"}},{"joiningDate":{"gt":"2023-01-01"}},{"salary":{"gt":20000}}]}&sort={"field":"name","order":"asc"}&pagination={"page":1,"pageSize":2}
```

**Example Response:**
```json
{
  "data": [
    {
      "id": "emp_789",
      "name": "Carol Lee",
      "department": "Finance",
      "joiningDate": "2023-05-10",
      "salary": 25000
    }
  ]
}
```

---

## 3. Aggregate Employees

**GET** `/api/v1/employee/aggregate`

**Description:**  
Aggregate employees by group and operation (e.g., count by department).

**Query Parameters:**
- `group_by` (string, required): Field to group by (e.g., `department`).
- `aggregate_field` (string, required): Field to aggregate (e.g., `id`).
- `aggregate_op` (string, required): Operation (e.g., `count`).
- `filter` (string, optional): JSON string for advanced filtering.
- `fields` (string, optional): Comma-separated list of fields to include in the response.

**Example Request:**
```
GET /api/v1/employee/aggregate?group_by=department&aggregate_field=id&aggregate_op=count&filter={"and":[{"status":{"eq":"active"}}]}
```

**Example Response:**
```json
{
  "data": [
    {
      "department": { "id": "dept_1", "name": "Finance" },
      "count_id": 5,
      "employees": [
        { "id": "emp_123", "name": "Alice Johnson" },
        { "id": "emp_456", "name": "Bob Smith" }
      ]
    }
  ]
}
```

---

## 4. Count Employees

**GET** `/api/v1/employee/count`

**Description:**  
Get the number of employees matching the filter.

**Query Parameters:**
- `filter` (string, optional): JSON string for advanced filtering.
- `fields` (string, optional): (Accepted for consistency, but not used.)

**Example Request:**
```
GET /api/v1/employee/count?filter={"and":[{"status":{"eq":"active"}}]}
```

**Example Response:**
```json
{
  "count": 42
}
```

---

## 5. Get All Employees

**GET** `/api/v1/employee`

**Description:**  
Retrieve all employees (not paginated).

**Example Request:**
```
GET /api/v1/employee
```

**Example Response:**
```json
[
  {
    "id": "emp_123",
    "name": "Alice Johnson",
    "workEmail": "alice.johnson@example.com",
    "department": { "id": "dept_1", "name": "Finance" },
    "joiningDate": "2021-05-01",
    "status": "active"
  }
]
```

---

## 6. Get Employee by ID

**GET** `/api/v1/employee/{id}`

**Description:**  
Fetch details for a specific employee.

**Example Request:**
```
GET /api/v1/employee/emp_123
```

**Example Response:**
```json
{
  "id": "emp_123",
  "name": "Alice Johnson",
  "workEmail": "alice.johnson@example.com",
  "department": { "id": "dept_1", "name": "Finance" },
  "joiningDate": "2021-05-01",
  "status": "active"
}
```

---

## 7. Create Employee

**POST** `/api/v1/employee`

**Description:**  
Add a new employee.

**Example Request:**
```json
{
  "name": "John Doe",
  "workEmail": "john.doe@example.com",
  "departmentId": "dept_1",
  "joiningDate": "2023-01-01",
  "status": "active"
}
```

**Example Response:**
```json
{
  "id": "emp_456",
  "name": "John Doe",
  "workEmail": "john.doe@example.com",
  "department": { "id": "dept_1", "name": "Finance" },
  "joiningDate": "2023-01-01",
  "status": "active"
}
```

---

## 8. Update Employee

**PUT** `/api/v1/employee/{id}`

**Description:**  
Update an existing employee.

**Example Request:**
```json
{
  "name": "Jane Doe",
  "status": "on-leave"
}
```

**Example Response:**  
`204 No Content`

---

## 9. Delete Employee

**DELETE** `/api/v1/employee/{id}`

**Description:**  
Remove an employee from the system.

**Example Response:**  
`204 No Content`

---

## 10. Get Employee Schema

**GET** `/api/v1/employee/schema`

**Description:**  
Get the schema for the employee entity, including fields, relationships, and endpoints.

**Example Request:**
```
GET /api/v1/employee/schema
```

**Example Response:**
```json
{
  "entity": "employee",
  "description": "Master data for employees in the organization.",
  "version": "1.0",
  "endpoints": [ ... ],
  "primary_key": "Id",
  "fields": [ ... ]
}
```

---

## Filtering Syntax

- Use the `filter` parameter as a JSON string.
- Example: `filter={"and":[{"department":{"eq":"Finance"}},{"status":{"eq":"active"}}]}`
- Supported operators: `eq`, `neq`, `gt`, `gte`, `lt`, `lte`, `in`

---

## Field Selection

- Use the `fields` parameter to limit which fields are returned.
- Example: `fields=id,name,joiningDate`

---

## Notes

- All endpoints return LLM/agent-friendly JSON.
- All navigation properties are flattened to avoid cycles.
- For advanced queries, always use the `filter` and `fields` parameters. 