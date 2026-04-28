# AssetFlow

Two ASP.NET applications, one database, one shared business-logic library. Built to make the migration patterns from my [.NET Framework to modern .NET blog post](https://qurrat2.github.io/2026/04/27/dotnet-framework-to-net8-migration/) inspectable as code rather than abstract claims.

> **Status:** Work in progress. The legacy side (.NET Framework 4.8 + WebForms + Web API 2) is complete. The modern API side (ASP.NET Core 10 + EF Core + JWT) and the README's full blog-mapping table are next.

## Why this exists

Most migration write-ups stay abstract. This repo is the opposite: a tiny working solution where every "before" pattern from the blog post has, or will have, a corresponding "after" pattern, both running against the same SQL Server database. Recruiters and engineers can clone it, build both sides, and read the diffs.

## Solution layout

```
AssetFlow/
├── shared/AssetFlow.Core/           netstandard2.0 — entities, services, contracts
├── legacy/AssetFlow.Legacy.Web/     net48 — WebForms + Web API 2 + Forms auth + ADO.NET
├── modern/AssetFlow.Api/            net10.0 — Kestrel + JWT + EF Core + Swagger (pending)
├── database/                        SQL Server schema, stored procs, seed data
└── tests/                           xUnit (Core unit tests passing; API integration tests pending)
```

## What's working today

- `AssetFlow.Core` (netstandard2.0) referenced by both apps. Pure C# business logic with `IAssetService`, repository interfaces, contracts. Unit-tested.
- `AssetFlow.Legacy.Web`: WebForms 4.8 app with Login, dashboard, asset detail (with friendly URL via `MapPageRoute`), admin-only assignment flow, logout. Web API 2 controller for JSON. Autofac DI, `IHttpModule`-based request logging, Forms authentication, ADO.NET stored procs via `Microsoft.Data.SqlClient`.
- Database schema, 5 stored procedures, seeded users + assets + departments.

## What's coming

- ASP.NET Core 10 API on the modern side (mirrors the legacy domain through EF Core, JWT, middleware, Swagger).
- Integration tests for the modern endpoints.
- Full README with a blog-claim → file-path mapping table.
- Optionally: a YARP gateway demonstrating per-route Strangler Fig migration.

## Running the legacy side today

### Prerequisites

- Visual Studio 2022 or 2026 with the **ASP.NET and web development** workload (and .NET Framework 4.8 SDK + targeting pack)
- .NET 10 SDK
- SQL Server LocalDB (ships with VS) or any SQL Server 2019+

### Setup

```bash
# 1. Apply schema, sprocs, and seeds (replace instance name as needed)
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database/01-schema.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database/02-sprocs.sql
sqlcmd -S "(localdb)\MSSQLLocalDB" -i database/03-seeds.sql

# 2. Run the shared library tests to confirm Core is healthy
dotnet test tests/AssetFlow.Core.Tests
```

### Launch the legacy app

Open `AssetFlow.sln` in Visual Studio. Set `AssetFlow.Legacy.Web` as the startup project. Press F5. The browser opens at `https://localhost:44xxx/Login.aspx`.

### Default credentials

| Username | Password       | Role  | Department |
|----------|----------------|-------|------------|
| `admin`  | `Password123!` | Admin | (sees all) |
| `alice`  | `Password123!` | Staff | IT (1)     |
| `bob`    | `Password123!` | Staff | Operations (2) |

## License

MIT. See [LICENSE](LICENSE).
