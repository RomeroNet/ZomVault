using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Backup;

public class BackupModel
{
    public int Id { get; set; }
    public required DateTime CreatedAt { get; init; }
    public required string Filename { get; init; }
    public required int CompressionLevel { get; init; }
    
    public required int SourceId { get; init; }
    public SaveSourceModel Source { get; set; } = null!;

    public string FilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "ZomVault",
        Source.Name,
        Filename
    );
}