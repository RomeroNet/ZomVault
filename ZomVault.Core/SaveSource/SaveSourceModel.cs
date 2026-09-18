using ZomVault.Core.Backup;

namespace ZomVault.Core.SaveSource;

public class SaveSourceModel
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public required string Path { get; init; }

    public ICollection<BackupModel> Backups { get; set; } = [];
}