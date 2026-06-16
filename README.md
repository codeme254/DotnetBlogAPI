# Blog API

Initialize user secrets:

```bash
dotnet user-secrets init
```

Set DB Connection String Secret:

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=BlogDB;Username=USERNAME;Password=PASSWORD;"
```