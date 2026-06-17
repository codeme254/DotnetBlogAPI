# Blog API

A RESTful Blog API built with ASP.NET Core, Entity Framework Core and PostgreSQL.

## Prerequisites

Make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- PostgreSQL
- Your preferred API client (Postman, Insomnia, etc.)

## Configuration

This project uses **.NET User Secrets** for storing sensitive configuration values during development.

### Initialize User Secrets

From the project directory, run:

```bash
dotnet user-secrets init
```

This creates a user secrets configuration file that is stored outside the project repository.

### Configure the Database Connection
Set your PostgreSQL connection string:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=BlogDB;Username=YOUR_USERNAME;Password=YOUR_PASSWORD;Include Error Detail=true;"
```

Replace:
- `YOUR_USERNAME` with your PostgreSQL username
- `YOUR_PASSWORD` with your PostgreSQL password

Example:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=BlogDB;Username=postgres;Password=password123;Include Error Detail=true;"
```

### Database Setup
Apply EF Core migrations:

```bash
dotnet ef database update
```

All models are there, so in case migrations don't exist:

Create the migrations
```bash
dotnet ef migrations add InitialCreate
```

Then apply the migrations to the database:
```bash
dotnet ef database update
```