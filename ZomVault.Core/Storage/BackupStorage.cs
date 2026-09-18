namespace ZomVault.Core.Storage;

public class BackupStorage : IBackupStorage
{
    public void Store(Stream archive, string destination, string filename)
    {
        Directory.CreateDirectory(destination);
        
        using var file = File.Create(Path.Combine(destination, filename));
        
        archive.CopyTo(file);
    }
}