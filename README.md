# Series Observations

Short instructions for running the project locally.

## Requirements

- Windows
- Visual Studio 2022 with .NET Framework 4.8 support
- PostgreSQL 18
- Node.js 20.19+ or 22.13+

## 1. Database

Install PostgreSQL 18 on Windows and create the empty database:

```powershell
cd Series.Database\Series.Database\Scripts\PostgreSql
psql -U postgres -f .\000_create_database.sql
```

By default, the backend expects:

```text
Host: 127.0.0.1
Port: 5432
Database: SeriesTest
User: postgres
Password: postgres
```

If your password is different, update the connection string in `Series.Api/Series.Api/Web.config`.

## 2. Backend

Open the solution in Visual Studio 2022:

```text
Series.Api/Series.Api.sln
```

Then:

1. Restore NuGet packages if Visual Studio does not do it automatically.
2. Run the `Series.Api` project with IIS Express.
3. On the first database access, Entity Framework will apply migrations and seed sample data.

Expected API URL:

```text
https://localhost:44328
```

Quick checks:

```text
https://localhost:44328/api/health
https://localhost:44328/api/series/tree
https://localhost:44328/api/observations?seriesId=4
```

## 3. Frontend

In a separate terminal:

```bash
cd frontend/series-client
npm install
cp .env.example .env.local
npm run dev
```

The `.env.local` file must point to the running backend:

```text
VITE_API_BASE_URL=https://localhost:44328/
```

Open the frontend:

```text
http://localhost:5173
```

