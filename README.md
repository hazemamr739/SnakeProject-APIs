# SnakeProject APIs

Production-style ASP.NET Core Web API for selling digital PlayStation products, managing PSN code inventory, checkout/cart flow, and orders.

---

## Overview

`SnakeProject` provides:
- Authentication with JWT + refresh token
- Role/permission-based authorization
- Category and product management
- PSN code lifecycle management (`Available -> Reserved -> Sold`)
- Cart and checkout flow
- Order lifecycle management

Built with `.NET 9` and clean layered architecture.

---

## Tech Stack

- **Framework:** ASP.NET Core Web API (`net9.0`)
- **Database:** SQL Server + Entity Framework Core
- **Identity:** ASP.NET Core Identity
- **Auth:** JWT Bearer + refresh tokens
- **Authorization:** Policy-based permissions (`HasPermission`)
- **Validation:** FluentValidation
- **Mapping:** Mapster
- **API docs/testing:** Swagger + Postman collection

---

## Solution Structure

- `SnakeProject-BE` → API host (`Program`, controllers, auth config)
- `SnakeProject.Application` → DTOs, contracts, validators, error models
- `SnakeProject.Domain` → entities and enums
- `SnakeProject.Infrastructure` → EF Core context, migrations, service implementations
- `SnakeProject.Shared` → shared abstractions/utilities

---

## Key Features

### Authentication & Authorization
- Register, login, refresh token, revoke refresh token
- Forgot/reset password endpoints
- Permission-protected endpoints using `HasPermission`
- Startup seed for roles and permissions (`Admin`, `Member`)

### Business Modules
- **Categories:** CRUD
- **Products:** CRUD with paging/filter/sort
- **PSN Codes:** CRUD + reserve/release/sell + inventory summary
- **Cart:** add/remove items, clear cart, checkout
- **Orders:** create from cart, list, get by id, update status, cancel

---

## API Base URL

- Local: `https://localhost:7168`
- Swagger: `https://localhost:7168/swagger`

---

## Configuration

Set values in `SnakeProject-BE/appsettings.json` or environment variables:

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`
- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:DurationInMinutes`
- `AdminSeed:Email`
- `AdminSeed:Password`

> **Important:** Never commit real secrets. Use **User Secrets** for local development and secure secret storage in production.

---

## Local Setup

### 1) Restore
```powershell
dotnet restore
```

### 2) Apply migrations
```powershell
dotnet ef database update --project SnakeProject.Infrastructure --startup-project SnakeProject-BE
```

### 3) Run API
```powershell
dotnet run --project SnakeProject-BE
```

### 4) Open Swagger
Go to:
- `https://localhost:7168/swagger`

---

## Endpoint Groups

- `Auth` → `/api/auth/*`
- `Account` → `/me/*`
- `Categories` → `/api/category/*`
- `Products` → `/api/product/*`
- `PSN Codes` → `/api/psncode/*`
- `Cart` → `/api/cart/*`
- `Orders` → `/api/order/*`

Detailed frontend integration guide:
- `docs/frontend-api-guide.md`

---

## Main Enums (Quick Reference)

### ProductType
- `1 = Account`
- `2 = Subscription`
- `3 = PsnCode`

### InventoryStatus
- `1 = Available`
- `2 = Reserved`
- `3 = Sold`

### OrderStatus
- `1 = Pending`
- `2 = Paid`
- `3 = Processing`
- `4 = Completed`
- `5 = Cancelled`
- `6 = Failed`

### Currency
- `1 = USD`
- `2 = UAE`
- `3 = KSA`
- `4 = EGP`

### AccessType
- `1 = FullAccount`
- `2 = Primary`
- `3 = Secondary`

---

## Postman

Use:
- `postman/SnakeProject-APIs.postman_collection.json`

Recommended testing order:
1. Login
2. Save/use bearer token
3. Test protected endpoints

---

## Troubleshooting

### 401 Unauthorized
- Missing/expired/invalid JWT token.

### 403 Forbidden
- Token is valid but user lacks required permission.
- Re-login after role/permission changes.

### EF/Migration errors
- Ensure connection string is valid.
- Re-run migrations command.
- Confirm model and database are in sync.

---

## Contributing

1. Create a feature branch
2. Make focused commits
3. Open pull request with clear description

---

Add your license here (for example: MIT).

