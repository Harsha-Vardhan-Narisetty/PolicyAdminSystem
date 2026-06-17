# Authentication and Authorization

## Purpose

This document explains how authentication and authorization are implemented in the Policy Admin System.

The application uses:

- BCrypt for password hashing
- JWT (JSON Web Tokens) for authentication
- Role-based authorization for access control
- Claims-based identity for user information
- Audit fields using the logged-in user's identity

---

# Authentication Flow

Authentication verifies the identity of a user.

The process works as follows:

User Login Request
↓
Validate Email
↓
Verify Password Using BCrypt
↓
Generate JWT Token
↓
Return Token To Client
↓
Client Uses Token For Future Requests

---

# User Registration

Endpoint:

POST /api/Users/register

Purpose:

Creates a new user account.

Process:

1. User submits registration details.
2. Password is hashed using BCrypt.
3. User record is saved to the database.
4. User information is returned.

Example Request:

{
    "firstName": "Harsha",
    "lastName": "Vardhan",
    "email": "harsha@gmail.com",
    "password": "Harsha123"
}

---

# BCrypt Password Hashing

Passwords are never stored as plain text.

During registration:

Password
↓
BCrypt HashPassword()
↓
Store Hashed Value In Database

Example:

Plain Password:

Harsha123

Stored Value:

$2a$11$xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

Benefits:

- Protects user passwords
- Prevents password disclosure
- Industry-standard security approach

---

# User Login

Endpoint:

POST /api/Users/login

Purpose:

Authenticates a registered user.

Process:

1. Find user by email.
2. Verify password using BCrypt.
3. Generate JWT token.
4. Return login response.

Example Request:

{
    "email": "harsha@gmail.com",
    "password": "Harsha123"
}

---

# JWT Token Generation

JWT tokens are generated after successful login.

Token generation is handled by:

TokenService

Location:

PolicyAdmin.Infrastructure
└── Services
    └── TokenService.cs

The token contains:

- UserId
- Email
- Role

Example Claims:

UserId = 5

Email = harsha@gmail.com

Role = Admin

---

# JWT Configuration

JWT settings are stored in:

appsettings.json

Example:

"JwtSettings": {
  "SecretKey": "YourSecretKey",
  "Issuer": "PolicyAdminAPI",
  "Audience": "PolicyAdminClient",
  "ExpiryMinutes": 60
}

Purpose:

- Centralized JWT configuration
- Easy maintenance
- Environment-specific configuration

---

# JWT Authentication

JWT Authentication validates incoming tokens.

Configuration Location:

Program.cs

Authentication Process:

Incoming Request
↓
Read JWT Token
↓
Validate Signature
↓
Validate Issuer
↓
Validate Audience
↓
Validate Expiry
↓
Create Authenticated User

If validation fails:

401 Unauthorized

is returned automatically.

---

# Authorization

Authorization determines what an authenticated user is allowed to do.

Authentication
↓
Identity Established
↓
Authorization
↓
Permission Check

---

# Role-Based Authorization

The application supports role-based access control.

Roles:

- User
- Admin

Implementation:

[Authorize(Roles = "Admin")]

Example:

Create Policy Holder

Update Policy Holder

Delete Policy Holder

are restricted to Admin users.

Read operations can be accessed by authenticated users.

---

# HTTP Status Codes

401 Unauthorized

Meaning:

User is not authenticated.

Example:

Request made without JWT token.

---

403 Forbidden

Meaning:

User is authenticated but lacks permission.

Example:

User role attempting to access Admin-only endpoint.

---

500 Internal Server Error

Meaning:

Unexpected application error.

Handled by:

ExceptionHandlingMiddleware

---

# Claims-Based Identity

Claims are stored inside the JWT token.

Examples:

ClaimTypes.NameIdentifier

ClaimTypes.Email

ClaimTypes.Role

These claims are available after authentication.

Example:

User.FindFirst(ClaimTypes.Role)

Purpose:

- Identify current user
- Support authorization
- Support auditing

---

# CurrentUserService

Purpose:

Provides access to the logged-in user's identity from the service layer.

Interface:

ICurrentUserService

Implementation:

CurrentUserService

Responsibilities:

- Read UserId claim
- Expose current user's identity
- Support auditing

Example:

_currentUserService.UserId

---

# Audit Fields

PolicyHolder entity supports auditing.

Fields:

CreatedDate

CreatedBy

ModifiedDate

ModifiedBy

Purpose:

Track:

- Who created a record
- When it was created
- Who modified a record
- When it was modified

Implementation:

JWT
↓
Claims
↓
CurrentUserService
↓
Audit Fields

Example:

CreatedBy = LoggedInUserId

ModifiedBy = LoggedInUserId

---

# Security Summary

The application implements:

✓ BCrypt Password Hashing

✓ JWT Authentication

✓ JWT Authorization

✓ Role-Based Authorization

✓ Claims-Based Identity

✓ Audit Fields

✓ Global Exception Handling

This provides a secure and enterprise-ready authentication and authorization framework.