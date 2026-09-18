using ZomVault.Core.Backup;
using ZomVault.Core.Database;
using ZomVault.Core.SaveSource;
using ZomVault.Core.Tests.Helper;
using ZomVault.Core.Tests.ObjectMother;

namespace ZomVault.Core.Tests.Backup;

public class BackupRepositoryTest
{
    [Fact]
    public void List_test()
    {
        using var db = DatabaseTestHelper.CreateContext();

        var sources = SaveSourceModelObjectMother.Get(2);
        var source = sources[0];
        var emptySource = sources[1];
        
        db.Sources.AddRange(sources);
        db.SaveChanges();
        
        var expectedList = BackupModelObjectMother.Get(source, 10);
        
        db.Backups.AddRange(expectedList);
        db.SaveChanges();

        var repository = new BackupRepository(db);

        var resultList = repository.GetWhereSourceName(source.Name);
        var emptyResultList = repository.GetWhereSourceName(emptySource.Name);
        
        Assert.Equal(expectedList.Count, resultList.Count);
        Assert.Empty(emptyResultList);

        foreach (var expected in expectedList)
        {
            var result = resultList.Single(x => x.Id == expected.Id);
            
            Assert.Equal(expected.CreatedAt, result.CreatedAt);
            Assert.Equal(expected.Filename, result.Filename);
            Assert.Equal(expected.CompressionLevel, result.CompressionLevel);
            Assert.Equal(expected.SourceId, result.SourceId);
            Assert.Equal(source, result.Source);
        }
    }

    [Fact]
    public void Add_test()
    {
        using var db = DatabaseTestHelper.CreateContext();

        var source = PrepareSource(db);
        
        var backup = BackupModelObjectMother.Get(source);
        
        var repository = new BackupRepository(db);
        
        repository.Add(backup);
        
        var result = db.Backups.Single(x => x.Id == backup.Id);
        
        Assert.Equal(backup.CreatedAt, result.CreatedAt);
        Assert.Equal(backup.Filename, result.Filename);
        Assert.Equal(backup.CompressionLevel, result.CompressionLevel);
        Assert.Equal(backup.SourceId, result.SourceId);
        Assert.Equal(source, result.Source);
    }

    [Fact]
    public void Delete_test()
    {
        using var db = DatabaseTestHelper.CreateContext();

        var source = PrepareSource(db);
        
        var backup = BackupModelObjectMother.Get(source);
        
        var repository = new BackupRepository(db);
        
        db.Backups.Add(backup);
        db.SaveChanges();
        
        repository.Delete(backup);
        
        var result = db.Backups.SingleOrDefault(x => x.Id == backup.Id);
        
        Assert.Null(result);
    }

    private SaveSourceModel PrepareSource(ZomVaultDatabaseContext db)
    {
        var source = SaveSourceModelObjectMother.Get();
        
        db.Sources.Add(source);
        db.SaveChanges();
        
        return source;
    }
}