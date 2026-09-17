namespace ZomVault.Core.Backup;

public class Backup
{
    public int Id { get; set; }
    public required DateTime CreatedAt { get; init; }
    public required string Filename { get; init; }
    public required int CompressionLevel { get; init; }
    
    public required int SourceId { get; init; }
    public SaveSource.SaveSource Source { get; init; } = null!;
}