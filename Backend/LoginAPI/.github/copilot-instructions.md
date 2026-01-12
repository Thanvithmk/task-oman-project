# GitHub Copilot instructions for LoginAPI

**Purpose:** Help AI coding agents become productive quickly in this repository. Focus on concrete, discoverable facts: how to run the app, important files, coding patterns, and integration points.

## Quick start ✅
- Restore & build: `dotnet restore && dotnet build`
- Run (dev):
  - Use the launch profile: `dotnet run --launch-profile "http"` (HTTP: http://localhost:5106)
  - Or run normally and use `ASPNETCORE_URLS`/`ASPNETCORE_ENVIRONMENT` env vars, e.g.:
    - `ASPNETCORE_URLS="http://localhost:5106" ASPNETCORE_ENVIRONMENT=Development dotnet run`
- Health-check: there is no dedicated health endpoint; call the API endpoints directly (see examples below).

## Key files & layout 🔧
- `Program.cs` — minimal WebApplication setup; registers controllers and enables HTTPS redirection.
- `Controllers/AuthController.cs` — login endpoint: POST `/api/auth/login`.
- `Models/UserLogin.cs` — request model used by login endpoint (`Username`, `Password`).
- `appsettings.json` / `appsettings.Development.json` — configuration; **ConnectionStrings:OracleDB** holds the Oracle DB connection string.
- `LoginAPI.csproj` — target framework `net10.0`, packages include `Oracle.ManagedDataAccess.Core`.
- `Properties/launchSettings.json` — local launch profiles and default ports.
- `LoginAPI.http` — quick HTTP snippets; useful for crafting requests in VS Code REST clients.

## Architecture & integration notes 🧭
- This is a minimal ASP.NET Core Web API using Controllers (not minimal endpoint lambdas).
- Database integration uses raw ADO.NET style queries against Oracle via `Oracle.ManagedDataAccess.Core` (no EF Core).
- The controller reads the connection string via injected `IConfiguration`:
  - Key used: `ConnectionStrings:OracleDB` (example in `appsettings.json`).
- Example of the SQL pattern in `AuthController`:
```csharp
string sql = "SELECT COUNT(*) FROM USERS WHERE USERNAME=:u AND PASSWORD=:p";
cmd.Parameters.Add(new OracleParameter("u", login.Username));
cmd.Parameters.Add(new OracleParameter("p", login.Password));
```
- Responses: returns `Ok("Login Successful")` or `Unauthorized("Invalid Credentials")`.

## Important, discoverable constraints & gotchas ⚠️
- No unit or integration tests present in the repo. Expect to add tests if requested.
- Passwords are compared directly in SQL (plain text comparison). This is the current behavior—do not assume hashing without explicit code changes.
- The app expects an accessible Oracle instance. The sample connection string in `appsettings.json` points to `localhost:1521/XEPDB1`.
- No migrations or database schema files are present—schema assumptions must be inferred from code (e.g., table `USERS` with `USERNAME` and `PASSWORD` columns).

## How to exercise the login endpoint (examples) 💡
- cURL example:
```bash
curl -X POST http://localhost:5106/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"Username":"alice","Password":"secret"}'
```
- VS Code: use `LoginAPI.http` as a starting snippet and replace host/port if needed.

## Development notes for AI agents 🤖
- Edit targets are small and localized: controller logic, adding validation, or replacing ADO.NET with a repository/EF layer are all feasible changes.
- Preserve existing behavior unless asked to change security semantics (e.g., introduce password hashing) — such changes can be breaking and need tests & config updates.
- When changing DB access, update `appsettings.json` docs/examples and consider adding fallback environment variable `ConnectionStrings__OracleDB` for CI.

---

If you'd like, I can: (1) open a PR with this file added, (2) expand sections with inline references to exact line numbers, or (3) add quick automated checks (e.g., a tiny integration test scaffold). Which would you prefer?