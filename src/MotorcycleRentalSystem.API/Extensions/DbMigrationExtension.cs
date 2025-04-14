using Microsoft.EntityFrameworkCore;

namespace MotorcycleRentalSystem.Extensions;

public static class DbMigrationExtension
{
    public static WebApplication MigrateDatabase<TContext>(this WebApplication app) where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();
        db.Database.Migrate();
        return app;
    }
}