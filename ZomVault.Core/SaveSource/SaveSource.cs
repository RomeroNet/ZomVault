namespace ZomVault.Core.SaveSource;

public class SaveSource
{
    public int Id { get; set; }
    public required string Name { get; init; }
    public required string Path { get; init; }

    public ICollection<Backup.Backup> Backups { get; set; } = [];
}