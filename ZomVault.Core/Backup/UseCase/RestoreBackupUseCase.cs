using ZomVault.Core.Backup.Compression;

namespace ZomVault.Core.Backup.UseCase;

public class RestoreBackupUseCase(
    ICompressionAlgorithm compressionAlgorithm
)
{
    public void Restore(BackupModel backup)
    {
        compressionAlgorithm.Extract(backup);
    }
}