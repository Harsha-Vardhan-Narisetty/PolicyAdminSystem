# Middleware and Exception Handling

This document explains:
- ASP.NET Core middleware pipeline
- custom middleware
- centralized exception handling
- logging flow
- request lifecycle

Middleware is one of the most important concepts in ASP.NET Core.

---

# What Is Middleware?

Middleware is software that sits between:
- incoming HTTP request
- outgoing HTTP response

Every request passes through middleware components before reaching controllers.

Example flow:

```text
Client Request
    ↓
Middleware 1
    ↓
Middleware 2
    ↓
Middleware 3
    ↓
Controller
```

After controller response:

```text
Controller Response
    ↓
Middleware 3
    ↓
Middleware 2
    ↓
Middleware 1
    ↓
Client Response
```

---

# Middleware Pipeline

ASP.NET Core processes requests using a middleware pipeline.

Middleware components are executed in the order they are registered.

Current middleware registration happens inside:

```text
Program.cs
```

---

# Current Middleware Pipeline

Current request flow:

```text
Client Request
    ↓
ExceptionHandlingMiddleware
    ↓
Authorization Middleware
    ↓
Controller
```

Response flow:

```text
Controller Response
    ↓
Authorization Middleware
    ↓
ExceptionHandlingMiddleware
    ↓
Client Response
```

---

# Why Middleware Is Important

Middleware is used for:
- exception handling
- logging
- authentication
- authorization
- request validation
- response modification
- rate limiting
- caching

Middleware enables centralized processing.

---

# Custom Middleware

The application currently uses custom middleware:

```text
ExceptionHandlingMiddleware.cs
```

Location:

```text
PolicyAdmin.API/Middleware
```

---

# Purpose of ExceptionHandlingMiddleware

This middleware handles:
- unhandled exceptions
- centralized error logging
- standardized error responses

Without middleware:
- application may crash
- inconsistent error responses occur
- debugging becomes difficult

---

# Why Centralized Exception Handling Is Important

Enterprise applications should NOT handle exceptions individually in every controller.

Instead:
- middleware catches exceptions globally
- logs them centrally
- returns standardized responses

Benefits:
- cleaner controllers
- centralized logging
- easier maintenance
- consistent API responses

---

# Middleware Structure

Custom middleware contains:

```csharp
public class ExceptionHandlingMiddleware
```

The main execution method is:

```csharp
InvokeAsync(HttpContext context)
```

This method executes for every request.

---

# Request Execution Flow

When request enters middleware:

```text
Request enters middleware
    ↓
Middleware calls next middleware
    ↓
Controller executes
    ↓
Exception occurs (if any)
    ↓
Middleware catches exception
    ↓
Logs exception
    ↓
Returns standardized error response
```

---

# try-catch Flow

Middleware uses:

```csharp
try
{
    await _next(context);
}
catch(Exception ex)
{
}
```

Explanation:

| Code | Purpose |
|---|---|
| _next(context) | Calls next middleware/controller |
| try | Monitors request execution |
| catch | Captures unhandled exceptions |

---

# Example Exception Scenario

Inside service layer:

```csharp
throw new Exception("Test exception from service layer");
```

The exception travels upward through layers:

```text
Service Layer
    ↓
Controller
    ↓
Middleware catches exception
```

Middleware prevents application crash.

---

# Logging Flow

Current middleware logs exceptions using:

```csharp
_logger.LogError(ex,
    "An exception occurred in the application.");
```

This logs:
- exception message
- stack trace
- source file
- line number

---

# Example Logged Exception

Example output:

```text
fail: PolicyAdmin.API.Middleware.ExceptionHandlingMiddleware[0]
      An exception occurred in the application.
      Message: Test exception from service layer
```

Stack trace shows:
- exact file
- exact line number
- request execution path

This is extremely useful for debugging.

---

# Stack Trace

Example stack trace:

```text
PolicyHolderService.cs:line 18
PolicyHoldersController.cs:line 21
ExceptionHandlingMiddleware.cs:line 25
```

This helps developers identify:
- where exception started
- how request traveled
- which layer failed

---

# Standardized Error Response

Middleware returns standardized JSON response.

Example:

```json
{
  "success": false,
  "message": "An unexpected error occurred."
}
```

This prevents exposing sensitive internal details to clients.

---

# Why We Do NOT Return Full Exception To Client

Returning full exception details may expose:
- database structure
- internal implementation
- file paths
- server information

This creates security risks.

Therefore:
- full details are logged internally
- safe response is returned externally

---

# Middleware Registration

Middleware is registered inside:

```text
Program.cs
```

Example:

```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
```

Order matters in middleware registration.

Middleware executes in registration order.

---

# Request Lifecycle Summary

Current request lifecycle:

```text
Client Request
    ↓
ExceptionHandlingMiddleware
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
```

Response lifecycle:

```text
SQL Server
    ↓
Repository
    ↓
Service
    ↓
Controller
    ↓
Middleware
    ↓
Client Response
```

---

# Enterprise Concepts Demonstrated

Current implementation demonstrates:

- ASP.NET Core Middleware
- Custom Middleware
- Centralized Exception Handling
- Structured Logging
- Request Pipeline
- Response Pipeline
- Layered Architecture
- Error Standardization

---

# Benefits of Current Middleware Design

The current middleware design provides:

- centralized exception handling
- cleaner controller code
- easier debugging
- better maintainability
- consistent error responses
- enterprise-level logging
- improved scalability

---

# Future Middleware Enhancements

Future middleware may include:
- JWT authentication
- request logging
- correlation IDs
- performance monitoring
- rate limiting
- audit logging
- API throttling
- response compression

The middleware pipeline is highly extensible.