using ZomVault.Core.Backup.Compression;
using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Tests.Backup.Compression;

public class CompressionAlgorithmTest
{
    [Fact]
    public void Test_create()
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
}