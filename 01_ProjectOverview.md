# Policy Admin System

## Project Overview

Policy Admin System is an enterprise-level backend application developed using ASP.NET Core Web API and SQL Server following Clean Architecture principles.

The purpose of this system is to manage insurance policy holder information through REST APIs.

Currently, the application supports:

* Creating policy holders
* Retrieving policy holders
* Updating policy holders
* Soft deleting policy holders

The project is designed in a scalable and maintainable enterprise architecture so that additional modules such as:

* Policies
* Claims
* Payments
* Authentication
* Authorization
* Audit Logs
* Notifications

can be added in the future.

---

# Technology Stack

| Technology            | Purpose                                      |
| --------------------- | -------------------------------------------- |
| ASP.NET Core Web API  | Backend API framework                        |
| SQL Server            | Database                                     |
| Entity Framework Core | ORM for database operations                  |
| Swagger               | API testing and documentation                |
| Git & GitHub          | Version control                              |
| Visual Studio 2022    | IDE                                          |
| Clean Architecture    | Project structure and separation of concerns |

---

# Architecture Style

The application follows Clean Architecture.

The solution is divided into multiple layers/projects:

* API Layer
* Application Layer
* Domain Layer
* Persistence Layer
* Infrastructure Layer
* Shared Layer

Each layer has its own responsibility.

This separation helps:

* maintainability
* scalability
* testability
* loose coupling
* enterprise-level organization

---

# Current Features Implemented

## Policy Holder APIs

### GET

Retrieve all active policy holders.

### POST

Create a new policy holder.

### PUT

Update existing policy holder information.

### DELETE

Soft delete a policy holder.

---

# Soft Delete

The project uses soft delete instead of hard delete.

Instead of physically removing records from the database, records are marked as inactive using:

```text id="2jlwmd"
IsActive = false
```

This approach is commonly used in enterprise applications for:

* audit purposes
* recovery
* compliance
* historical tracking

---

# Current Architecture Flow

```text id="1jlwma"
Client Request
    ↓
Controller Layer
    ↓
Service Layer
    ↓
Repository Layer
    ↓
Entity Framework DbContext
    ↓
SQL Server Database
```

---

# Current Status

The backend currently supports:

* Clean Architecture
* CRUD operations
* DTO usage
* Repository Pattern
* Service Layer
* Middleware
* Exception Handling
* Logging
* API Response Wrapper
* Soft Delete

Authentication and Authorization using JWT tokens will be implemented next.
