using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ZomVault.Core.Database;

namespace ZomVault.Core.SaveSource;

public class SourceRepository(ZomVaultDatabaseContext db)
{
    public List<SaveSourceModel> GetAll()
    {
        return [.. db.Sources];
    }

    public void Add(SaveSourceModel source)
    {
        db.Sources.Add(source);
        db.SaveChanges();
    }

    public void Remove(string name)
    {
        var deleted = db.Sources
            .Where(s => s.Name == name)
            .ExecuteDelete();

        if (deleted == 0)
        {
            throw new InvalidOperationException($"Source {name} not found");
        }

        db.SaveChanges();
    }
}