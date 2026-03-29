# SnakeProject APIs

`SnakeProject APIs` is a layered ASP.NET Core Web API project for managing:

- Authentication and user profile
- Categories
- Products
- PSN codes inventory lifecycle
- Shopping cart
- Orders

Built with `.NET 9`, `Entity Framework Core`, `ASP.NET Core Identity`, `JWT`, and permission-based authorization.

---

## Solution Structure

- `SnakeProject-BE` → API host (controllers, auth config, startup)
- `SnakeProject.Application` → DTOs, service contracts, validators, error models
- `SnakeProject.Domain` → entities, enums
- `SnakeProject.Infrastructure` → EF Core context, services, repositories, migrations
- `SnakeProject.Shared` → shared abstractions/utilities

---

## Tech Stack

- `.NET 9` (`net9.0`)
- ASP.NET Core Web API
- EF Core + SQL Server
- ASP.NET Core Identity
- JWT Bearer Authentication
- Claims/Policy authorization (`HasPermission`)
- FluentValidation
- Mapster
- Swagger / OpenAPI

---

## Features

### Authentication
- Register
- Login
- Refresh token
- Revoke refresh token
- Forgot/reset password
- Resend confirmation email flow (API ready)

### Authorization
- Permission-based endpoint protection using `HasPermission`
- Startup role/permission seeding:
  - `Admin`
  - `Member`
- Role claims seeded through `RolePermissionSeeder`

### Business Modules
- Category CRUD
- Product CRUD with paging/filter/sort
- PSN code lifecycle:
  - create / update / delete
  - reserve / release / sell
  - reserve next available by denomination
  - inventory summary
- Cart operations
- Order operations

---

## Base URL

- Local: `https://localhost:7168`
- Swagger: `https://localhost:7168/swagger`

---

## Configuration

Configure `SnakeProject-BE/appsettings.json` (or environment variables):

- `ConnectionStrings:DefaultConnection`
- `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience`, `Jwt:DurationInMinutes`
- `AdminSeed:Email`, `AdminSeed:Password` (optional, for first admin seed)

> Do not commit real secrets. Use User Secrets in development and environment variables/secret manager in production.

---

## Run

1. Set configuration values.
2. Apply migrations:

```powershell
dotnet ef database update --project SnakeProject.Infrastructure --startup-project SnakeProject-BE
```

3. Run API:

```powershell
dotnet run --project SnakeProject-BE
```

---

## Main Endpoint Groups

- `Auth`: `/api/auth/*`
- `Account`: `/me/*`
- `Categories`: `/api/category/*`
- `Products`: `/api/product/*`
- `PSN Codes`: `/api/psncode/*`
- `Cart`: `/api/cart/*`
- `Orders`: `/api/order/*`

For full request/response bodies and integration notes, use:
- `docs/frontend-api-guide.md`

---

## Main Enum Values

### `ProductType`
- `1 = Account`
- `2 = Subscription`
- `3 = PsnCode`

### `InventoryStatus`
- `1 = Available`
- `2 = Reserved`
- `3 = Sold`

### `OrderStatus`
- `1 = Pending`
- `2 = Paid`
- `3 = Processing`
- `4 = Completed`
- `5 = Cancelled`
- `6 = Failed`

### `Currency`
- `1 = USD`
- `2 = UAE`
- `3 = KSA`
- `4 = EGP`

### `AccessType`
- `1 = FullAccount`
- `2 = Primary`
- `3 = Secondary`

---

## Postman

Use collection:
- `postman/SnakeProject-APIs.postman_collection.json`

Recommended order:
1. Login
2. Save/use bearer token
3. Call protected endpoints

---

## Troubleshooting

### `401 Unauthorized`
- Invalid/expired token.
- Missing `Authorization: Bearer {token}`.

### `403 Forbidden`
- Token is valid but missing required permission claim.
- Login again after role/permission changes to get fresh claims.

### `Invalid column name ...`
- Database schema not in sync. Re-run migrations.

---

## License

Add your license (`MIT`, `Apache-2.0`, etc.).

