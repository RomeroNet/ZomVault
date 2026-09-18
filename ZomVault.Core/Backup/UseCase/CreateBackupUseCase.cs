using ZomVault.Core.Archive;
using ZomVault.Core.Storage;

namespace ZomVault.Core.Backup.UseCase;

public class CreateBackupUseCase(
    IArchiver archiver,
    IBackupStorage backupStorage,
    BackupRepository repository
)
{
    public void Create(SaveSource.SaveSourceModel source)
    {
        var archive = archiver.Create(source);

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
    }
}