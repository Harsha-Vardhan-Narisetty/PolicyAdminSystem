# CRUD APIs

This document explains the Policy Holder APIs implemented in the Policy Admin System.

CRUD stands for:

| Operation | HTTP Method |
|---|---|
| Create | POST |
| Read | GET |
| Update | PUT |
| Delete | DELETE |

The application currently supports CRUD operations for Policy Holders.

---

# Base URL

Example local API URL:

```text
https://localhost:44301/api/PolicyHolders
```

---

# Common Response Structure

All APIs return responses using:

```text
ApiResponse<T>
```

Standard response format:

```json
{
  "success": true,
  "message": "Operation successful",
  "data": {}
}
```

Benefits:
- consistent API structure
- easier frontend integration
- standardized error handling

---

# 1. GET — Retrieve All Policy Holders

## Endpoint

```http
GET /api/PolicyHolders
```

---

## Purpose

Retrieves all active policy holders.

Soft deleted records are automatically excluded.

---

## Controller Method

File:

```text
PolicyHoldersController.cs
```

Method:

```csharp
[HttpGet]
public async Task<IActionResult> GetAllPolicyHolders()
```

---

## Service Layer

File:

```text
PolicyHolderService.cs
```

Method:

```csharp
GetAllPolicyHoldersAsync()
```

---

## Repository Layer

File:

```text
PolicyHolderRepository.cs
```

Method:

```csharp
GetAllAsync()
```

---

## Database Query

Example:

```csharp
_context.PolicyHolders
    .Where(x => x.IsActive)
```

Only active records are returned.

---

## Success Response

HTTP Status:

```text
200 OK
```

Example response:

```json
{
  "success": true,
  "message": "Policy holders retrieved successfully",
  "data": [
    {
      "policyHolderId": 1,
      "fullName": "John Doe",
      "email": "john@gmail.com",
      "phoneNumber": "9876543210",
      "city": "Chennai"
    }
  ]
}
```

---

# 2. POST — Create Policy Holder

## Endpoint

```http
POST /api/PolicyHolders
```

---

## Purpose

Creates a new policy holder.

---

## Request DTO

File:

```text
CreatePolicyHolderRequestDto.cs
```

---

## Request Body Example

```json
{
  "firstName": "Harsha",
  "lastName": "Vardhan",
  "dateOfBirth": "1998-05-10",
  "gender": "Male",
  "email": "harsha@gmail.com",
  "phoneNumber": "9876543210",
  "addressLine1": "MG Road",
  "addressLine2": "Apartment 5A",
  "city": "Chennai",
  "state": "Tamil Nadu",
  "postalCode": "600001",
  "country": "India"
}
```

---

## Validation

Validation attributes are used in DTO.

Examples:
- Required
- StringLength
- EmailAddress
- Phone

ASP.NET Core automatically validates request model.

---

## Success Response

HTTP Status:

```text
200 OK
```

Example:

```json
{
  "success": true,
  "message": "Policy holder created successfully",
  "data": {
    "policyHolderId": 10,
    "fullName": "Harsha Vardhan",
    "email": "harsha@gmail.com",
    "phoneNumber": "9876543210",
    "city": "Chennai"
  }
}
```

---

## Invalid Request Response

HTTP Status:

```text
400 Bad Request
```

Occurs when validation fails.

Example:
- missing required fields
- invalid email format
- invalid phone number

---

# 3. PUT — Update Policy Holder

## Endpoint

```http
PUT /api/PolicyHolders/{id}
```

Example:

```http
PUT /api/PolicyHolders/1
```

---

## Purpose

Updates existing policy holder information.

---

## Request DTO

File:

```text
UpdatePolicyHolderRequestDto.cs
```

---

## Request Body Example

```json
{
  "firstName": "Updated",
  "lastName": "User",
  "dateOfBirth": "1990-01-01",
  "gender": "Male",
  "email": "updated@gmail.com",
  "phoneNumber": "9999999999",
  "addressLine1": "Updated Address",
  "addressLine2": null,
  "city": "Bangalore",
  "state": "Karnataka",
  "postalCode": "560001",
  "country": "India"
}
```

---

## Update Flow

Update process:

```text
Find existing record
    ↓
Validate existence
    ↓
Update properties
    ↓
Save changes
    ↓
Return response
```

---

## Success Response

HTTP Status:

```text
200 OK
```

Example:

```json
{
  "success": true,
  "message": "Policy holder updated successfully",
  "data": {
    "policyHolderId": 1,
    "fullName": "Updated User",
    "email": "updated@gmail.com",
    "phoneNumber": "9999999999",
    "city": "Bangalore"
  }
}
```

---

## Record Not Found Response

HTTP Status:

```text
404 Not Found
```

Example:

```json
{
  "success": false,
  "message": "Policy holder with ID 999 not found"
}
```

---

# 4. DELETE — Soft Delete Policy Holder

## Endpoint

```http
DELETE /api/PolicyHolders/{id}
```

Example:

```http
DELETE /api/PolicyHolders/1
```

---

## Purpose

Soft deletes a policy holder.

The record is NOT physically removed from database.

Instead:

```text
IsActive = false
```

---

# Why Soft Delete Is Used

Soft delete is commonly used in enterprise applications for:
- audit history
- recovery
- compliance
- historical tracking
- accidental deletion prevention

---

## Delete Flow

```text
Find active record
    ↓
Validate existence
    ↓
Set IsActive = false
    ↓
Save changes
    ↓
Return response
```

---

## Success Response

HTTP Status:

```text
200 OK
```

Example:

```json
{
  "success": true,
  "message": "Policy holder deleted successfully",
  "data": "Deleted policy holder ID: 1"
}
```

---

## Already Deleted / Not Found Response

HTTP Status:

```text
404 Not Found
```

Example:

```json
{
  "success": false,
  "message": "Policy holder with ID 1 not found"
}
```

---

# Soft Delete Filtering

Soft deleted records are filtered using:

```csharp
.Where(x => x.IsActive)
```

This ensures deleted records are hidden from normal API responses.

---

# Validation Summary

The application currently validates:

- required fields
- string length
- email format
- phone number format

Validation is handled automatically by ASP.NET Core.

---

# Current CRUD Architecture

```text
Client
    ↓
Controller
    ↓
Service
    ↓
Repository
    ↓
DbContext
    ↓
SQL Server
```

---
# Authentication APIs

## Register User

POST /api/Users/register

Purpose:

Creates a new user account.

---

## Login User

POST /api/Users/login

Purpose:

Authenticates user and returns JWT token.

Response Includes:

- UserId
- FullName
- Email
- Role
- JWT Token
- Token Expiration

# Important Enterprise Concepts Used

The CRUD implementation currently demonstrates:

- REST APIs
- Clean Architecture
- Repository Pattern
- Service Layer Pattern
- DTO usage
- Soft Delete
- Dependency Injection
- Middleware
- Validation
- Standardized API Responses
- Entity Framework Core