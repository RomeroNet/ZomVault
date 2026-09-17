using ZomVault.Core.Archive;

namespace ZomVault.Core.Backup.UseCase;

public class CreateBackupUseCase(BackupRepository repository)
{
    public void Create(SaveSource.SaveSource source)
    {
        var archive = Archiver.Create(source);

        var backup = new Backup()
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
        
        Directory.CreateDirectory(destination);

        var file = File.Create(Path.Combine(destination, backup.Filename));

        archive.CopyTo(file);
        
        repository.Add(backup);
    }
}