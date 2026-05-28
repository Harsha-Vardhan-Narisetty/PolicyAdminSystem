# Database Design and Entity Framework Core

This document explains:
- database-first approach
- SQL Server integration
- Entity Framework Core
- DbContext
- scaffolding
- repositories
- entity mapping

The Policy Admin System uses:
- SQL Server
- Entity Framework Core
- Database First approach

---

# What Is Database First Approach?

In Database First approach:

```text
Database is created first
    ↓
Tables are created in SQL Server
    ↓
Entity Framework scaffolds C# models
```

This is different from:
- Code First approach
- Model First approach

---

# Why Database First Was Chosen

Database First is commonly used in enterprise applications because:
- database teams may already exist
- existing databases may already exist
- easier integration with legacy systems
- DBAs control schema design

This project follows enterprise-style backend development.

---

# Current Database

Current database name:

```text
PolicyAdminDB
```

Database engine:

```text
SQL Server
```

---

# Current Main Table

Current table:

```text
PolicyHolders
```

This table stores:
- policy holder personal details
- contact information
- address information
- active status

---

# Current Important Columns

Example columns:

| Column | Purpose |
|---|---|
| PolicyHolderId | Primary key |
| FirstName | Policy holder first name |
| LastName | Policy holder last name |
| DateOfBirth | Date of birth |
| Email | Email address |
| PhoneNumber | Contact number |
| IsActive | Soft delete flag |
| CreatedDate | Record creation date |

---

# Primary Key

Current primary key:

```text
PolicyHolderId
```

This uniquely identifies each record.

Example:

| PolicyHolderId | FirstName |
|---|---|
| 1 | John |
| 2 | Harsha |

---

# Soft Delete Design

The application uses:

```text
IsActive
```

column for soft delete.

Instead of physically deleting rows:

```text
DELETE FROM PolicyHolders
```

the application performs:

```text
IsActive = false
```

Benefits:
- preserves history
- prevents accidental data loss
- supports audit tracking

---

# SQL Server Integration

The application connects to SQL Server using connection string.

Location:

```text
appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection":
    "Server=localhost;Database=PolicyAdminDB;
     Trusted_Connection=True;
     TrustServerCertificate=True;"
}
```

---

# Connection String Explanation

| Property | Purpose |
|---|---|
| Server | SQL Server instance |
| Database | Database name |
| Trusted_Connection | Windows Authentication |
| TrustServerCertificate | Accept local SSL certificate |

---

# What Is Entity Framework Core?

Entity Framework Core (EF Core) is Microsoft's ORM framework.

ORM means:

```text
Object Relational Mapper
```

EF Core converts:
- C# objects
- LINQ queries

into:
- SQL queries

This avoids writing raw SQL for most operations.

---

# Benefits of Entity Framework Core

EF Core provides:
- easier database access
- LINQ querying
- automatic SQL generation
- entity tracking
- async operations
- database abstraction

---

# Scaffolding

Scaffolding generates:
- entities
- DbContext

from existing database.

This project used scaffold command.

---

# Scaffold Command

Example scaffold command:

```powershell
Scaffold-DbContext
"Server=localhost;Database=PolicyAdminDB;
Trusted_Connection=True;TrustServerCertificate=True;"
Microsoft.EntityFrameworkCore.SqlServer
-OutputDir Models
-ContextDir Contexts
-Context PolicyAdminDbContext
-DataAnnotations
```

---

# What Scaffolding Generated

Scaffolding generated:

```text
PolicyHolder.cs
PolicyAdminDbContext.cs
```

These were later reorganized into clean architecture structure.

---

# Entity Class

Current entity:

```text
PolicyHolder.cs
```

Location:

```text
PolicyAdmin.Domain/Entities
```

Entity represents database table.

Example:

```text
PolicyHolders table
    ↔
PolicyHolder entity
```

---

# Entity Responsibilities

Entity contains:
- database column mappings
- entity properties
- data annotations

Example:

```csharp
public string FirstName { get; set; }
```

maps to SQL column:

```sql
FirstName
```

---

# Data Annotations

Entity uses annotations such as:

```csharp
[StringLength(100)]
```

and:

```csharp
[Column(TypeName = "datetime")]
```

These help EF Core understand:
- column types
- max lengths
- database rules

---

# DbContext

Current DbContext:

```text
PolicyAdminDbContext.cs
```

Location:

```text
PolicyAdmin.Persistence/Contexts
```

DbContext is one of the MOST important EF Core concepts.

---

# Purpose of DbContext

DbContext acts as bridge between:
- C# application
- SQL Server database

It manages:
- entity tracking
- database connections
- querying
- saving changes

---

# DbSet

DbContext contains:

```csharp
public virtual DbSet<PolicyHolder> PolicyHolders { get; set; }
```

DbSet represents table access.

Example:

```text
DbSet<PolicyHolder>
```

represents:

```text
PolicyHolders table
```

---

# Repository Interaction

Repository uses DbContext for database operations.

Example:

```csharp
_context.PolicyHolders.ToListAsync();
```

Flow:

```text
Repository
    ↓
DbContext
    ↓
SQL Server
```

---

# LINQ Queries

Repository uses LINQ queries.

Example:

```csharp
_context.PolicyHolders
    .Where(x => x.IsActive)
```

EF Core converts LINQ into SQL.

Equivalent SQL:

```sql
SELECT *
FROM PolicyHolders
WHERE IsActive = 1
```

---

# SaveChangesAsync

Changes are saved using:

```csharp
await _context.SaveChangesAsync();
```

This commits:
- inserts
- updates
- soft deletes

to database.

---

# Async Database Operations

Application uses async methods such as:
- ToListAsync
- FirstOrDefaultAsync
- SaveChangesAsync

Benefits:
- better scalability
- non-blocking operations
- improved performance

Enterprise applications heavily use async programming.

---

# Current CRUD Database Operations

Current repository supports:

| Operation | Database Action |
|---|---|
| Get | SELECT |
| Create | INSERT |
| Update | UPDATE |
| Delete | Soft DELETE (UPDATE IsActive) |

---

# Example Request Flow

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
DbContext
    ↓
Entity Framework Core
    ↓
SQL Server
```

---

# Why Repository Pattern Is Used

Repository pattern:
- separates database logic
- improves maintainability
- improves testability
- abstracts EF Core from services

Service layer should NOT directly access DbContext.

---

# Current Enterprise Concepts Demonstrated

Current implementation demonstrates:

- SQL Server integration
- Database First approach
- Entity Framework Core
- ORM concepts
- DbContext usage
- DbSet usage
- LINQ querying
- Repository pattern
- Async database operations
- Soft delete architecture

---

# Future Enhancements

Future database enhancements may include:
- stored procedures
- migrations
- indexing
- relationships
- foreign keys
- transaction management
- audit tables
- database views
- caching
- pagination

The current architecture is scalable for future growth.