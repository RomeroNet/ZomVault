namespace ZomVault.Core.Backup.Compression;

public interface ICompressionAlgorithm
{
    Stream Compress(SaveSource.SaveSourceModel source);
}