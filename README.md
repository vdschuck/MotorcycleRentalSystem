# MotorcycleRentalSystem


## Create database migrations

```
dotnet ef migrations add <migration_name> --project src/MotorcycleRentalSystem.Infrastructure --startup-project src/MotorcycleRentalSystem.API

```

## Start the application and the database.

```
docker compose up --build
```