namespace ZomVault.Core.Backup.Storage;

public interface IBackupStorage
{
    void Store(Stream archive, string destination, string filename);

    void Restore(BackupModel backup);

    void Delete(string path);
}