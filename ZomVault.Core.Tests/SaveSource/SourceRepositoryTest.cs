using Bogus;
using ZomVault.Core.SaveSource;
using ZomVault.Core.Tests.Helper;
using ZomVault.Core.Tests.ObjectMother;

namespace ZomVault.Core.Tests.SaveSource;

public class SourceRepositoryTest
{
    [Fact]
    public void List_test()
    {
        using var db = DatabaseTestHelper.CreateContext();

        var expectedList = SaveSourceModelObjectMother.Get(10);
        
        db.Sources.AddRange(expectedList);
        db.SaveChanges();
        
        var repository = new SourceRepository(db);

        var resultList = repository.GetAll();
        
        Assert.Equal(10, resultList.Count());

        foreach (var expected in expectedList)
        {
            var result = resultList.Single(x => x.Id == expected.Id);
            
            Assert.Equal(expected.Name, result.Name);
            Assert.Equal(expected.Path, result.Path);
        }
    }

    [Fact]
    public void Add_test()
    {
        using var db = DatabaseTestHelper.CreateContext();

        var expected = SaveSourceModelObjectMother.Get();
        
        var repository = new SourceRepository(db);

        repository.Add(expected);
        
        var result = db.Sources.Single(x => x.Id == expected.Id);
        
        Assert.Equal(expected.Name, result.Name);
        Assert.Equal(expected.Path, result.Path);
    }

    [Fact]
    public void Remove_test()
    {
        using var db = DatabaseTestHelper.CreateContext();
        
        var item = SaveSourceModelObjectMother.Get();
        
        db.Sources.Add(item);
        db.SaveChanges();
        
        var repository = new SourceRepository(db);
        
        repository.Remove(item.Name);
        
        var result = db.Sources.SingleOrDefault(x => x.Id == item.Id);
        
        Assert.Null(result);
    }

    [Fact]
    public void Remove_Not_Found_Test()
    {
        using var db = DatabaseTestHelper.CreateContext();
        
        var item = SaveSourceModelObjectMother.Get();
        
        var repository = new SourceRepository(db);

        var exception = Assert.Throws<InvalidOperationException>(
            () => repository.Remove(item.Name)
        );

        Assert.Equal(
            $"Source {item.Name} not found",
            exception.Message
        );
    }
}