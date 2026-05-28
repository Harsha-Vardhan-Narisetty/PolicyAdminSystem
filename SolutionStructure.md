# Solution Structure

The Policy Admin System solution is divided into multiple projects following Clean Architecture principles.

Each project has a specific responsibility.

This separation helps maintain:
- clean code organization
- loose coupling
- scalability
- maintainability
- testability

---

# Solution Projects

The solution currently contains the following projects:

1. PolicyAdmin.API
2. PolicyAdmin.Application
3. PolicyAdmin.Domain
4. PolicyAdmin.Infrastructure
5. PolicyAdmin.Persistence
6. PolicyAdmin.Shared

---

# 1. PolicyAdmin.API

## Purpose

This is the presentation layer of the application.

It exposes REST APIs to external clients such as:
- Swagger
- Angular frontend
- Postman
- Mobile applications

---

## Responsibilities

The API layer is responsible for:

- Receiving HTTP requests
- Returning HTTP responses
- Calling application services
- Configuring middleware
- Dependency injection configuration
- Authentication and authorization (future)
- API routing

---

## Current Folders

### Controllers

Contains API controllers.

Example:

```text
PolicyHoldersController.cs
```

Controllers receive incoming HTTP requests and call the service layer.

---

### Middleware

Contains custom middleware components.

Example:

```text
ExceptionHandlingMiddleware.cs
```

Middleware handles:
- centralized exception handling
- logging
- request/response processing

---

### Properties

Contains launch settings.

Example:

```text
launchSettings.json
```

Used for:
- application URLs
- environment configuration
- debugging profiles

---

### Program.cs

Application startup file.

Responsible for:
- configuring services
- dependency injection
- middleware registration
- Swagger configuration
- database connection setup

---

### appsettings.json

Contains application configuration.

Currently stores:
- SQL Server connection string
- logging configuration

---

# 2. PolicyAdmin.Application

## Purpose

Contains application business logic.

This layer acts as the core business orchestration layer between:
- API layer
- Persistence layer

---

## Responsibilities

- Business logic
- DTO definitions
- Service interfaces
- Service implementations
- API response models
- Repository contracts

---

## Current Folders

### DTOs

DTO = Data Transfer Object

Used to transfer data between layers.

Current DTOs:
- CreatePolicyHolderRequestDto
- UpdatePolicyHolderRequestDto
- PolicyHolderResponseDto

DTOs help:
- avoid exposing entities directly
- control API contracts
- improve security
- simplify responses

---

### Interfaces

Contains contracts/interfaces.

Current interfaces:
- IPolicyHolderService
- IPolicyHolderRepository

Interfaces help achieve:
- loose coupling
- dependency inversion
- testability

---

### Responses

Contains generic API response models.

Current file:

```text
ApiResponse.cs
```

Used to standardize API responses.

---

### Services

Contains business logic implementations.

Current file:

```text
PolicyHolderService.cs
```

Responsible for:
- business validations
- calling repositories
- DTO mapping
- response generation

---

# 3. PolicyAdmin.Domain

## Purpose

Contains core business entities.

This is the heart of the application.

The Domain layer should contain:
- entities
- enums
- domain rules
- business models

This layer should NOT depend on other layers.

---

## Current Folders

### Entities

Contains database entities/models.

Current entity:

```text
PolicyHolder.cs
```

Represents the PolicyHolders database table.

---

# 4. PolicyAdmin.Infrastructure

## Purpose

Reserved for external integrations and infrastructure services.

Currently not heavily used.

Future usage may include:
- email services
- file storage
- third-party APIs
- SMS services
- payment gateways

---

# 5. PolicyAdmin.Persistence

## Purpose

Handles database operations.

This layer communicates directly with SQL Server using Entity Framework Core.

---

## Responsibilities

- database access
- repositories
- DbContext
- Entity Framework configuration

---

## Current Folders

### Contexts

Contains Entity Framework DbContext.

Current file:

```text
PolicyAdminDbContext.cs
```

Acts as bridge between:
- C# entities
- SQL Server database

---

### Repositories

Contains repository implementations.

Current file:

```text
PolicyHolderRepository.cs
```

Responsible for:
- CRUD database operations
- querying data
- saving data
- soft delete implementation

---

# 6. PolicyAdmin.Shared

## Purpose

Contains reusable shared components.

Currently minimal.

Future usage may include:
- constants
- utility classes
- common helper methods
- shared enums
- shared models

---

# Current Request Flow

Current application flow:

```text
Client Request
    ?
Controller
    ?
Service Layer
    ?
Repository Layer
    ?
DbContext
    ?
SQL Server
```

---

# Architectural Benefits

The current architecture provides:

- Separation of concerns
- Maintainability
- Scalability
- Reusability
- Easier testing
- Better organization
- Enterprise-level structure