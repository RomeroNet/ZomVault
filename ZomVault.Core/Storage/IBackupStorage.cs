namespace ZomVault.Core.Storage;

public interface IBackupStorage
{
    void Store(Stream archive, string destination, string filename);
}