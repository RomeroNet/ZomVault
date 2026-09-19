using ZomVault.Core.Backup.Compression;
using ZomVault.Core.Backup.Storage;
using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Backup.UseCase;

public class CreateBackupUseCase(
    ICompressionAlgorithm compressionAlgorithm,
    IBackupStorage backupStorage,
    BackupRepository repository
)
{
    public BackupModel Create(SaveSourceModel source)
    {
        var archive = compressionAlgorithm.Compress(source);

        var backup = new BackupModel()
        {
            CreatedAt = DateTime.UtcNow,
            Filename = $"{DateTime.Now:yyyy_MM_dd_HH_mm_ss}.tar.zst",
            CompressionLevel = 10,
            SourceId = source.Id,
        };
        
        var destination = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ZomVault",
            source.Name
        );
        
        backupStorage.Store(archive, destination, backup.Filename);
        
        repository.Add(backup);
        
        return backup;
    }
}