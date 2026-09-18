using Moq;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.Storage;
using ZomVault.Core.Backup.UseCase;
using ZomVault.Core.Tests.Helper;
using ZomVault.Core.Tests.ObjectMother;

namespace ZomVault.Core.Tests.Backup.UseCase;

public class DeleteBackupUseCaseTest
{
    [Fact]
    public void Delete_test()
    {
        using var db = DatabaseTestHelper.CreateContext();
        var storage = new Mock<IBackupStorage>();
        var repository = new BackupRepository(db);

        var source = SaveSourceModelObjectMother.Get();
        db.Sources.Add(source);
        db.SaveChanges();

        var backup = BackupModelObjectMother.Get(source);
        db.Backups.Add(backup);
        db.SaveChanges();

        var useCase = new DeleteBackupUseCase(storage.Object, repository);

        useCase.Delete(backup);

        storage
            .Verify(x => x.Delete(
                Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "ZomVault",
                    source.Name,
                    backup.Filename
                )
            ));
    }
}