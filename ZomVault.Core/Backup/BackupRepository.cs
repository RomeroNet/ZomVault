using ZomVault.Core.Database;

namespace ZomVault.Core.Backup;

public class BackupRepository(ZomVaultDatabaseContext db)
{
    public List<Backup> GetWhereSourceName(string sourceName)
    {
        return db.Backups
            .Where(backup => backup.Source.Name == sourceName)
            .ToList();
    }

    public void Add(Backup backup)
    {
        db.Backups.Add(backup);
        db.SaveChanges();
    }
}