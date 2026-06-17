# Request Flow

This document explains how an HTTP request travels through the application.

Understanding request flow is one of the most important concepts in backend development.

The Policy Admin System follows a layered architecture.

Each layer has a specific responsibility.

---

# High-Level Flow

Current application flow:

```text
Client
    ↓
Controller
    ↓
Service Layer
    ↓
Repository Layer
    ↓
DbContext
    ↓
SQL Server Database
```

After database response:

```text
SQL Server
    ↓
DbContext
    ↓
Repository
    ↓
Service
    ↓
Controller
    ↓
Client Response
```

---

# Example Request Flow

Example API:

```http
GET /api/PolicyHolders
```

This request travels through multiple layers.

---

# Step 1 — Client Sends Request

The client can be:
- Swagger
- Angular frontend
- Postman
- Mobile application

Example request:

```http
GET https://localhost:44301/api/PolicyHolders
```

The request reaches the ASP.NET Core application.

---

# Step 2 — Routing Finds Controller

ASP.NET Core routing system checks:

```csharp
[Route("api/[controller]")]
```

inside:

```text
PolicyHoldersController.cs
```

The framework identifies:

```text
PolicyHoldersController
```

as the target controller.

Then it searches for matching HTTP method.

Example:

```csharp
[HttpGet]
```

This matches the GET request.

---

# Step 3 — Controller Receives Request

Controller method executes.

Example:

```csharp
public async Task<IActionResult> GetAllPolicyHolders()
```

Controller responsibilities:
- receive HTTP request
- validate incoming data
- call service layer
- return HTTP response

Controllers should NOT contain heavy business logic.

---

# Step 4 — Controller Calls Service Layer

Inside controller:

```csharp
var policyHolders =
    await _policyHolderService.GetAllPolicyHoldersAsync();
```

The controller delegates business processing to the service layer.

This follows:
- separation of concerns
- clean architecture principles

---

# Step 5 — Service Layer Executes Business Logic

File:

```text
PolicyHolderService.cs
```

The service layer handles:
- business rules
- validations
- DTO mapping
- response generation
- orchestration

Example operations:
- checking if record exists
- soft delete logic
- converting entities to DTOs
- preparing API responses

The service layer should NOT directly access SQL Server.

Instead, it communicates through repositories.

---

# Step 6 — Service Calls Repository

Example:

```csharp
await _policyHolderRepository.GetAllAsync();
```

The repository layer handles database operations.

This follows:
- repository pattern
- abstraction principles
- loose coupling

---

# Step 7 — Repository Accesses DbContext

File:

```text
PolicyAdminDbContext.cs
```

Repository uses Entity Framework Core DbContext.

Example:

```csharp
_context.PolicyHolders
```

DbContext acts as bridge between:
- C# code
- SQL Server database

---

# Step 8 — Entity Framework Generates SQL Query

Entity Framework Core converts LINQ query into SQL.

Example:

```csharp
_context.PolicyHolders
    .Where(x => x.IsActive)
    .ToListAsync();
```

may become SQL similar to:

```sql
SELECT *
FROM PolicyHolders
WHERE IsActive = 1
```

This SQL query is sent to SQL Server.

---

# Step 9 — SQL Server Executes Query

SQL Server:
- processes query
- retrieves records
- returns data

The result travels back through application layers.

---

# Step 10 — Repository Returns Data

Repository receives database data and returns entities to service layer.

Example returned entity:

```text
PolicyHolder
```

---

# Step 11 — Service Converts Entities to DTOs

Service layer maps entities into DTOs.

Example:

```csharp
new PolicyHolderResponseDto
{
    PolicyHolderId = policyHolder.PolicyHolderId,
    FullName = $"{policyHolder.FirstName} {policyHolder.LastName}"
}
```

Why DTOs are used:
- avoid exposing database entities
- improve security
- simplify API responses
- separate internal models from external contracts

---

# Step 12 — Service Returns ApiResponse

Service wraps response using:

```text
ApiResponse<T>
```

Example:

```csharp
return new ApiResponse<IEnumerable<PolicyHolderResponseDto>>
{
    Success = true,
    Message = "Policy holders retrieved successfully",
    Data = response
};
```

This standardizes API responses.

---

# Step 13 — Controller Returns HTTP Response

Controller returns:

```csharp
return Ok(response);
```

ASP.NET Core converts response object into JSON.

Example response:

```json
{
  "success": true,
  "message": "Policy holders retrieved successfully",
  "data": []
}
```

This JSON is returned to client.

---

# Middleware Participation

Before reaching controller, requests pass through middleware pipeline.

Current middleware:

```text
ExceptionHandlingMiddleware
```

Middleware can:
- log requests
- handle exceptions
- validate tokens
- modify requests/responses

Future middleware may include:
- JWT authentication
- authorization
- request logging
- rate limiting

---

# Important Architectural Concepts

## Separation of Concerns

Each layer has a dedicated responsibility.

Example:
- Controller → HTTP handling
- Service → business logic
- Repository → database operations

This improves maintainability.

---

## Loose Coupling

Layers communicate using interfaces.

Example:

```text
IPolicyHolderService
IPolicyHolderRepository
```

This improves:
- flexibility
- testability
- maintainability

---

## Dependency Injection

ASP.NET Core automatically injects dependencies.

Example:

```csharp
public PolicyHoldersController(
    IPolicyHolderService policyHolderService)
```

Dependency Injection reduces tight coupling.

---

# Current Request Flow Summary

```text
Client Request
    ↓
ASP.NET Core Routing
    ↓
Controller
    ↓
Service Layer
    ↓
Repository Layer
    ↓
DbContext
    ↓
SQL Server
    ↓
Repository
    ↓
Service
    ↓
Controller
    ↓
JSON Response
```

---

# Benefits of Current Architecture

The current architecture provides:

- clean code organization
- separation of concerns
- scalability
- maintainability
- easier debugging
- enterprise-level structure
- reusable components
- easier testing