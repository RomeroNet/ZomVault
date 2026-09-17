using Microsoft.EntityFrameworkCore;

namespace ZomVault.Core.Database;

public class ZomVaultDatabaseContext : DbContext
{
    public DbSet<SaveSource.SaveSource> Sources => Set<SaveSource.SaveSource>();
    public DbSet<Backup.Backup> Backups => Set<Backup.Backup>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ZomVault"
        );
        
        Directory.CreateDirectory(configDirectory);

        var databasePath = Path.Combine(configDirectory, "zomvault.db");
        
        optionsBuilder.UseSqlite($"Data Source={databasePath};");
    }
}