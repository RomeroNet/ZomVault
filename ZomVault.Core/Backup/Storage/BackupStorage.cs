namespace ZomVault.Core.Backup.Storage;

public class BackupStorage : IBackupStorage
{
    public void Store(Stream archive, string destination, string filename)
    {
        Directory.CreateDirectory(destination);
        
        using var file = File.Create(Path.Combine(destination, filename));
        
        archive.CopyTo(file);
    }

    public void Restore(BackupModel backup)
    {
        throw new NotImplementedException();
    }

    public void Delete(string path)
    {
        File.Delete(path);
    }
}