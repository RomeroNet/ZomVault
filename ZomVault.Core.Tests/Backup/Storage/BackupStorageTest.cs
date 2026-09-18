using ZomVault.Core.Backup.Storage;

namespace ZomVault.Core.Tests.Backup.Storage;

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

    [Fact]
    public void Delete_test()
    {
        var storage = new BackupStorage();
        
        var filePath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid().ToString()
        );
        
        File.WriteAllText(filePath, "Hello ZomVault!");
        
        Assert.True(File.Exists(filePath));
        
        storage.Delete(filePath);
        
        Assert.False(File.Exists(filePath));
    }
}