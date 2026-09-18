using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ZomVault.Core.Database;

namespace ZomVault.Core.Tests.Helper;

public static class DatabaseTestHelper
{
    public static ZomVaultDatabaseContext CreateContext()
    {
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<ZomVaultDatabaseContext>()
            .UseSqlite(connection)
            .Options;

        var context = new ZomVaultDatabaseContext(options);
        
        context.Database.EnsureCreated();

        return context;
    }
}