using Bogus;
using ZomVault.Core.Backup;
using ZomVault.Core.SaveSource;

namespace ZomVault.Core.Tests.ObjectMother;

public class BackupModelObjectMother
{
    public static BackupModel Get(SaveSourceModel source)
    {
        return GetFaker(source)
            .Generate();
    }

    public static List<BackupModel> Get(SaveSourceModel source, int count)
    {
        return GetFaker(source)
            .Generate(count);
    }

    private static Faker<BackupModel> GetFaker(SaveSourceModel source)
    {
        return new Faker<BackupModel>()
            .RuleFor(x => x.CreatedAt, f => f.Date.Recent())
            .RuleFor(x => x.Filename, f => f.System.FileName())
            .RuleFor(x => x.CompressionLevel, f => f.Random.Number(0, 20))
            .RuleFor(x => x.SourceId, f => source.Id);
    }
}