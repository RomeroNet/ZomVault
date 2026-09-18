using Moq;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.Compression;
using ZomVault.Core.Backup.Storage;
using ZomVault.Core.Backup.UseCase;
using ZomVault.Core.SaveSource;
using ZomVault.Core.Tests.Helper;
using ZomVault.Core.Tests.ObjectMother;

namespace ZomVault.Core.Tests.Backup.UseCase;

public class CreateBackupUseCaseTest
{
    [Fact]
    public void Create_Test()
    {
        using var db = DatabaseTestHelper.CreateContext();
        var archiver = new Mock<ICompressionAlgorithm>();
        var storage = new Mock<IBackupStorage>();

        var archive = new MemoryStream([1, 2, 3]);
        archiver
            .Setup(x => x.Compress(It.IsAny<SaveSourceModel>()))
            .Returns(archive);

        var repository = new BackupRepository(db);

        var useCase = new CreateBackupUseCase(
            archiver.Object,
            storage.Object,
            repository
        );

        var source = SaveSourceModelObjectMother.Get();
        
        db.Sources.Add(source);
        db.SaveChanges();
        
        useCase.Create(source);
        
        storage.Verify(
            x => x.Store(
                archive,
                It.Is<string>(path => path.Contains(source.Name)),
                It.Is<string>(filename => filename.EndsWith(".tar.zst"))
            ),
            Times.Once
        );

        var backup = db.Backups.Single();
        
        Assert.Equal(source.Id, backup.SourceId);
        Assert.Equal(10, backup.CompressionLevel);
        Assert.EndsWith(".tar.zst",  backup.Filename);
    }
}