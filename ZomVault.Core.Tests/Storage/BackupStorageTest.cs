using ZomVault.Core.Storage;

namespace ZomVault.Core.Tests.Storage;

public class BackupStorageTest
{
    [Fact]
    public void Store_test()
    {
        var storage = new BackupStorage();

        var archive = new MemoryStream([1, 2, 3]);

        var destination = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString()
        );

        var filename = "file";
        
        storage.Store(archive, destination, filename);
        
        var file = Path.Combine(destination, filename);
        
        Assert.True(File.Exists(file));
        
        var contents = File.ReadAllBytes(file);

        Assert.Equal(
            new byte[] { 1, 2, 3 },
            contents
        );
    }
}