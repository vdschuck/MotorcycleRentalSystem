using MotorcycleRentalSystem.Tests.Fixtures;

namespace MotorcycleRentalSystem.Tests.Infrastructure;

[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseContainerFixture>
{
}