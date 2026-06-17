# Dependency Injection

This document explains:
- Dependency Injection (DI)
- interfaces
- service registration
- constructor injection
- loose coupling
- service lifetimes

Dependency Injection is one of the most important concepts in ASP.NET Core.

The entire application architecture heavily relies on DI.

---

# What Is Dependency Injection?

Dependency Injection (DI) is a design pattern used to provide objects that a class depends on.

Instead of a class creating its own dependencies:

```csharp
var service = new PolicyHolderService();
```

ASP.NET Core automatically provides required dependencies.

This is called:

```text
Dependency Injection
```

---

# Why Dependency Injection Is Important

Dependency Injection helps achieve:
- loose coupling
- maintainability
- testability
- scalability
- cleaner architecture

Modern enterprise applications heavily rely on DI.

---

# Problem Without Dependency Injection

Without DI:

```csharp
public class PolicyHoldersController
{
    private readonly PolicyHolderService _service =
        new PolicyHolderService();
}
```

Problems:
- tightly coupled code
- difficult testing
- hardcoded dependencies
- difficult maintenance

---

# Solution Using Dependency Injection

Current implementation:

```csharp
public class PolicyHoldersController : ControllerBase
{
    private readonly IPolicyHolderService _policyHolderService;

    public PolicyHoldersController(
        IPolicyHolderService policyHolderService)
    {
        _policyHolderService = policyHolderService;
    }
}
```

ASP.NET Core automatically injects the dependency.

---

# Constructor Injection

The application currently uses:

```text
Constructor Injection
```

Dependencies are provided through constructor parameters.

Example:

```csharp
public PolicyHolderService(
    IPolicyHolderRepository policyHolderRepository)
{
    _policyHolderRepository = policyHolderRepository;
}
```

This is the most common DI approach in ASP.NET Core.

---

# Dependency Injection Flow

Current dependency flow:

```text
Controller
    ↓ depends on
IPolicyHolderService
    ↓ implemented by
PolicyHolderService
    ↓ depends on
IPolicyHolderRepository
    ↓ implemented by
PolicyHolderRepository
    ↓ depends on
PolicyAdminDbContext
```

ASP.NET Core automatically resolves dependencies.

---

# What Is Loose Coupling?

Loose coupling means:
- classes depend on abstractions
- not concrete implementations

Example:

```csharp
IPolicyHolderService
```

instead of:

```csharp
PolicyHolderService
```

Benefits:
- easier replacement
- easier testing
- better maintainability

---

# Interfaces

The application uses interfaces such as:

```text
IPolicyHolderService
IPolicyHolderRepository
```

Interfaces define contracts.

Example:

```csharp
public interface IPolicyHolderService
{
    Task<ApiResponse<IEnumerable<PolicyHolderResponseDto>>>
        GetAllPolicyHoldersAsync();
}
```

Any class implementing this interface must follow the contract.

---

# Interface Implementation

Example implementation:

```csharp
public class PolicyHolderService
    : IPolicyHolderService
{
}
```

Meaning:

```text
PolicyHolderService implements IPolicyHolderService
```

---

# Why Interfaces Are Important

Interfaces provide:
- abstraction
- loose coupling
- easier testing
- flexibility
- scalability

Future implementations can easily replace current implementation.

Example:

```text
PolicyHolderService
MockPolicyHolderService
CachedPolicyHolderService
```

All can implement same interface.

---

# Service Registration

Dependencies are registered inside:

```text
Program.cs
```

Example:

```csharp
builder.Services.AddScoped<
    IPolicyHolderRepository,
    PolicyHolderRepository>();

builder.Services.AddScoped<
    IPolicyHolderService,
    PolicyHolderService>();
```

This tells ASP.NET Core:

```text
Whenever IPolicyHolderService is requested,
provide PolicyHolderService
```

---

# Dependency Resolution

When controller requires:

```csharp
IPolicyHolderService
```

ASP.NET Core DI container:
- searches registrations
- creates object
- injects dependency automatically

This process is called:

```text
Dependency Resolution
```

---

# Built-in DI Container

ASP.NET Core includes built-in DI container.

The container manages:
- object creation
- object lifetime
- dependency graphs

Developers usually do NOT manually create objects.

---

# Service Lifetimes

ASP.NET Core supports different service lifetimes.

Main lifetimes:

| Lifetime | Description |
|---|---|
| Transient | New object every request |
| Scoped | Same object per HTTP request |
| Singleton | Single object for entire application |

---

# Current Lifetime Used

Current application uses:

```csharp
AddScoped()
```

Example:

```csharp
builder.Services.AddScoped<
    IPolicyHolderService,
    PolicyHolderService>();
```

---

# What Scoped Means

Scoped lifetime means:

```text
One object per HTTP request
```

Example:

```text
Request starts
    ↓
Single service instance created
    ↓
Used throughout request
    ↓
Disposed after request ends
```

This is ideal for:
- Entity Framework DbContext
- repositories
- business services

---

# Why Singleton Is NOT Used For DbContext

DbContext should NOT be Singleton because:
- DbContext is not thread-safe
- shared state issues may occur
- concurrency problems may occur

Scoped lifetime is safest for DbContext.

---

# Dependency Injection Example Flow

Current flow:

```text
HTTP Request
    ↓
Controller created
    ↓
ASP.NET injects IPolicyHolderService
    ↓
Service created
    ↓
ASP.NET injects IPolicyHolderRepository
    ↓
Repository created
    ↓
ASP.NET injects PolicyAdminDbContext
```

All dependencies are resolved automatically.

---

# Dependency Injection Benefits

Current architecture benefits from DI by providing:

- loose coupling
- easier maintenance
- cleaner architecture
- centralized dependency management
- improved scalability
- easier testing
- reusable services

---

# Relationship With Clean Architecture

Dependency Injection is essential for:
- Clean Architecture
- layered architecture
- separation of concerns

Layers communicate using abstractions/interfaces.

This keeps architecture flexible.

---

# Current Enterprise Concepts Demonstrated

Current implementation demonstrates:

- Dependency Injection
- Constructor Injection
- Interface-based architecture
- Loose Coupling
- Service Registration
- Dependency Resolution
- Scoped Services
- Clean Architecture principles

---

# Future Dependency Injection Enhancements

Future DI usage may include:
- JWT services
- email services
- caching services
- logging services
- external API integrations
- background jobs
- notification services

ASP.NET Core DI container is highly extensible.