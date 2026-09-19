using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Backup.Compression;

public interface ICompressionAlgorithm
{
    Stream Compress(SaveSourceModel source);

    void Extract(BackupModel backup);
}