using System.Formats.Tar;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.Compression;
using ZomVault.Core.SaveSource;
using ZomVault.Core.Tests.ObjectMother;
using ZstdNet;

namespace ZomVault.Core.Tests.Backup.Compression;

public class CompressionAlgorithmTest
{
    [Fact]
    public void Test_compress()
    {
        var sourceDirectory = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString()
        );

        Directory.CreateDirectory(sourceDirectory);

        File.WriteAllText(
            Path.Combine(sourceDirectory, "test.txt"),
            "Hello ZomVault.Core!"
        );

        var source = new SaveSourceModel
        {
            Name = "Test",
            Path = sourceDirectory
        };

        var archiver = new CompressionAlgorithm();

        using var archive = archiver.Compress(source);

        Assert.NotNull(archive);
        Assert.True(archive.Length > 0);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Test_decompress(bool sourceExistsBeforeDecompressing)
    {
        var sourcePath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString()
        );
        
        Directory.CreateDirectory(sourcePath);
        
        File.WriteAllText(
            Path.Combine(sourcePath, "test.txt"),
            "Hello ZomVault.Core!"
        );
        File.WriteAllText(
            Path.Combine(sourcePath, "test2.txt"),
            "Another hello ZomVault.Core!"
        );

        var source = new SaveSourceModel()
        {
            Name = "Test",
            Path = sourcePath
        };

        var backup = new BackupModel()
        {
            CreatedAt = DateTime.Now,
            Filename = "test.tar.zst",
            CompressionLevel = 10,
            SourceId = source.Id,
            Source = source
        };

        Directory.CreateDirectory(Path.Combine(
            Path.GetDirectoryName(backup.FilePath)!
        ));

        using (var file = File.Create(backup.FilePath))
        using (var compressor = new CompressionStream(file))
        {
            TarFile.CreateFromDirectory(
                sourcePath,
                compressor,
                false
            );
        }

        if (!sourceExistsBeforeDecompressing)
        {
            Directory.Delete(sourcePath, true);
        }
        
        var compressionAlgorithm = new CompressionAlgorithm();
        
        compressionAlgorithm.Extract(backup);
        
        Assert.True(Directory.Exists(sourcePath));
        Assert.True(File.Exists(backup.FilePath));
        Assert.Equal(
            "Hello ZomVault.Core!",
            File.ReadAllText(Path.Combine(sourcePath, "test.txt"))
        );
        Assert.Equal(
            "Another hello ZomVault.Core!",
            File.ReadAllText(Path.Combine(sourcePath, "test2.txt"))
        );
        
        Directory.Delete(Path.GetDirectoryName(backup.FilePath)!, true);
    }
}