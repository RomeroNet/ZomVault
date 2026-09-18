using Bogus;
using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Tests.ObjectMother;

public static class SaveSourceModelObjectMother
{
    public static SaveSourceModel Get()
    {
        return GetFaker()
            .Generate();
    }

    public static List<SaveSourceModel> Get(int count)
    {
        return GetFaker()
            .Generate(count);
    }

    private static Faker<SaveSourceModel> GetFaker()
    {
        return new Faker<SaveSourceModel>()
            .RuleFor(x => x.Name, f => f.Random.Word())
            .RuleFor(x => x.Path, f => f.System.DirectoryPath());
    }
}