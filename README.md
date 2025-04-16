# MotorcycleRentalSystem


## Create database migrations

```
dotnet ef migrations add <migration_name> --project src/MotorcycleRentalSystem.Infrastructure --startup-project src/MotorcycleRentalSystem.API

```

## Start the application and the database.

```
docker compose up --build
```

## Setup AWS locally

Ensure that the **AWS CLI** is configured locally with your credentials and that the settings in `appsettings.json` are correct

```
aws configure
```

for more details: [AWS documentation](https://docs.aws.amazon.com/cli/latest/userguide/getting-started-quickstart.html)