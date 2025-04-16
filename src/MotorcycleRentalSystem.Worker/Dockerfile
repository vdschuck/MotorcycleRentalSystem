FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY src/MotorcycleRentalSystem.Worker/MotorcycleRentalSystem.Worker.csproj MotorcycleRentalSystem.Worker/
COPY src/MotorcycleRentalSystem.Domain/MotorcycleRentalSystem.Domain.csproj MotorcycleRentalSystem.Domain/
COPY src/MotorcycleRentalSystem.Infrastructure/MotorcycleRentalSystem.Infrastructure.csproj MotorcycleRentalSystem.Infrastructure/

RUN dotnet restore MotorcycleRentalSystem.Worker/MotorcycleRentalSystem.Worker.csproj

COPY src/ ./

WORKDIR /src/MotorcycleRentalSystem.Worker
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish MotorcycleRentalSystem.Worker.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MotorcycleRentalSystem.Worker.dll"]
