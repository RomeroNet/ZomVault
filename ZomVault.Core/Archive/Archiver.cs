using System.Formats.Tar;
using ZstdNet;

namespace ZomVault.Core.Archive;

public class Archiver : IArchiver
{
    public Stream Create(SaveSource.SaveSourceModel source)
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
}