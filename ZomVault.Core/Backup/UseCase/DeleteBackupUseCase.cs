using ZomVault.Core.Backup.Storage;

namespace ZomVault.Core.Backup.UseCase;

public class DeleteBackupUseCase(
    IBackupStorage storage,
    BackupRepository repository
)
{
    public void Delete(BackupModel backup)
    {
        var path = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "ZomVault",
            backup.Source.Name,
            backup.Filename
        );
        
        storage.Delete(path);
        
        repository.Delete(backup);
    }
}