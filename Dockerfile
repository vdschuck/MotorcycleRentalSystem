FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ./src/MotorcycleRentalSystem.API/MotorcycleRentalSystem.API.csproj ./MotorcycleRentalSystem.API/
COPY ./src/MotorcycleRentalSystem.Application/MotorcycleRentalSystem.Application.csproj ./MotorcycleRentalSystem.Application/
COPY ./src/MotorcycleRentalSystem.Infrastructure/MotorcycleRentalSystem.Infrastructure.csproj ./MotorcycleRentalSystem.Infrastructure/
COPY ./src/MotorcycleRentalSystem.Domain/MotorcycleRentalSystem.Domain.csproj ./MotorcycleRentalSystem.Domain/

RUN dotnet restore ./MotorcycleRentalSystem.API/MotorcycleRentalSystem.API.csproj

COPY ./src/MotorcycleRentalSystem.API/ ./MotorcycleRentalSystem.API/
COPY ./src/MotorcycleRentalSystem.Application/ ./MotorcycleRentalSystem.Application/
COPY ./src/MotorcycleRentalSystem.Infrastructure/ ./MotorcycleRentalSystem.Infrastructure/
COPY ./src/MotorcycleRentalSystem.Domain/ ./MotorcycleRentalSystem.Domain/

WORKDIR /src/MotorcycleRentalSystem.API
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotorcycleRentalSystem.API.dll"]
