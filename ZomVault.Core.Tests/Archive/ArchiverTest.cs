using ZomVault.Core.Archive;
using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Tests.Archive;

public class ArchiverTest
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

        var archiver = new Archiver();

        using var archive = archiver.Create(source);

        Assert.NotNull(archive);
        Assert.True(archive.Length > 0);
    }
}