# Solution Structure

The solution follows Clean Architecture principles.

Projects are organized based on responsibilities and dependencies.

---

PolicyAdminSystem

│

├── PolicyAdmin.API

├── PolicyAdmin.Application

├── PolicyAdmin.Domain

├── PolicyAdmin.Infrastructure

├── PolicyAdmin.Persistence

└── PolicyAdmin.Shared

---

# Project Responsibilities

## PolicyAdmin.API

Purpose:

Entry point of the application.

Responsibilities:

- Controllers
- Middleware
- Authentication Configuration
- Authorization Configuration
- Dependency Injection Registration
- Swagger Configuration
- HTTP Request Handling

Key Components:

Controllers

- PolicyHoldersController
- UsersController

Middleware

- ExceptionHandlingMiddleware

Services

- CurrentUserService

Files

- Program.cs
- appsettings.json

---

## PolicyAdmin.Application

Purpose:

Contains business logic and application rules.

Responsibilities:

- Services
- Interfaces
- DTOs
- Application-level responses

Key Folders:

DTOs

Interfaces

Services

Responses

Important Files:

Interfaces

- IPolicyHolderService
- IUserService
- IPolicyHolderRepository
- IUserRepository
- ICurrentUserService

Services

- PolicyHolderService
- UserService

DTOs

- CreatePolicyHolderRequestDto
- UpdatePolicyHolderRequestDto
- PolicyHolderResponseDto

- RegisterUserRequestDto
- LoginRequestDto
- LoginResponseDto
- UserResponseDto

---

## PolicyAdmin.Domain

Purpose:

Contains core business entities.

Responsibilities:

- Domain Models
- Entity Definitions

Entities:

- PolicyHolder
- User

The Domain project contains no database logic or API logic.

---

## PolicyAdmin.Infrastructure

Purpose:

Contains infrastructure-related services.

Responsibilities:

- JWT Token Generation
- External Service Integrations

Services:

- TokenService

Authentication:

- JwtSettings

---

## PolicyAdmin.Persistence

Purpose:

Handles database operations.

Responsibilities:

- Entity Framework Core
- SQL Server Access
- Repositories
- DbContext

Key Components:

Contexts

- PolicyAdminDbContext

Repositories

- PolicyHolderRepository
- UserRepository

---

## PolicyAdmin.Shared

Purpose:

Shared components used across multiple projects.

Responsibilities:

- Shared Models
- Shared Utilities
- Common Constants

(Currently minimal but available for future expansion)

---

# Dependency Flow

Dependencies flow inward.

API
↓
Application
↓
Domain

Infrastructure and Persistence implement interfaces defined in Application.

This ensures loose coupling and maintainability.

---

# Authentication Components

User Authentication is implemented using:

- User Entity
- UserRepository
- UserService
- TokenService
- CurrentUserService
- JWT Authentication
- JWT Authorization

---

# Audit Components

Audit functionality is implemented using:

- CreatedDate
- CreatedBy
- ModifiedDate
- ModifiedBy

CurrentUserService retrieves the logged-in user information from JWT claims and populates audit fields automatically.

---

# Benefits Of This Structure

- Separation of Concerns
- Maintainability
- Testability
- Scalability
- Enterprise-Ready Design
- Easier Team Collaboration