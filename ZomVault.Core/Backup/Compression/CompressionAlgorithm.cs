using System.Formats.Tar;
using ZomVault.Core.SaveSource;
using ZstdNet;

namespace ZomVault.Core.Backup.Compression;

public class CompressionAlgorithm : ICompressionAlgorithm
{
    public Stream Compress(SaveSourceModel source)
    {
        var stream = new MemoryStream();
        var options = new CompressionOptions(10);

        using (var compressor = new CompressionStream(stream, options))
        {
            TarFile.CreateFromDirectory(
                source.Path,
                compressor,
                false
            );
        }

        stream.Position = 0;

        return stream;
    }

    public void Extract(BackupModel backup)
    {
        try
        {
            Directory.Delete(backup.Source.Path, true);
        }
        catch (DirectoryNotFoundException)
        {
        }

        Directory.CreateDirectory(backup.Source.Path);

        using var file = File.OpenRead(backup.FilePath);
        using var decompressor = new DecompressionStream(file);

        TarFile.ExtractToDirectory(
            decompressor,
            backup.Source.Path,
            true
        );
    }
}