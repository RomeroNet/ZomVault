using Microsoft.EntityFrameworkCore;
using ZomVault.Core.Database;

namespace ZomVault.Core.Backup;

public class BackupRepository(ZomVaultDatabaseContext db)
{
    public List<BackupModel> GetWhereSourceName(string sourceName)
    {
        return db.Backups
            .Where(backup => backup.Source.Name == sourceName)
            .ToList();
    }

    public void Add(BackupModel backup)
    {
        db.Backups.Add(backup);
        db.SaveChanges();
    }

    public void Delete(BackupModel backup)
    {
        db.Backups.Remove(backup);
        db.SaveChanges();
    }

    public BackupModel GetWhereFileName(string fileName)
    {
        return db.Backups
            .Include(x => x.Source)
            .Single(backup => backup.Filename == fileName);
    }
}