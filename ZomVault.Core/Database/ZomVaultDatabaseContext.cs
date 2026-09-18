using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ZomVault.Core.Backup;
using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Database;

[ExcludeFromCodeCoverage]
public class ZomVaultDatabaseContext : DbContext
{
    public DbSet<SaveSourceModel> Sources => Set<SaveSourceModel>();
    public DbSet<BackupModel> Backups => Set<BackupModel>();

    public ZomVaultDatabaseContext()
    {
    }

    public ZomVaultDatabaseContext(DbContextOptions<ZomVaultDatabaseContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        
        var configDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ZomVault"
        );
        
        Directory.CreateDirectory(configDirectory);

        var databasePath = Path.Combine(configDirectory, "zomvault.db");
        
        optionsBuilder.UseSqlite($"Data Source={databasePath};");
    }
}