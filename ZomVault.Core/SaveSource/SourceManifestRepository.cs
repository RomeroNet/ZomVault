using System.Text.Json;

namespace ZomVault.Core.SaveSource;

public class SourceManifestRepository
{
    private readonly string _filePath;

    public SourceManifestRepository()
    {
        var configDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ZomVault"
        );
        
        Directory.CreateDirectory(configDirectory);
        _filePath = Path.Combine(configDirectory, "sources.json");
    }

    public List<SaveSource> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return [];
        }

        var json = File.ReadAllText(_filePath);
        
        return JsonSerializer.Deserialize<List<SaveSource>>(json) ?? [];
    }

    public void Add(SaveSource source)
    {
        var sources = GetAll();

        if (sources.Any(x => x.Name.Equals(source.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException($"Source {source.Name} already exists.");
        }
        
        sources.Add(source);

        Save(sources);
    }

    public void Remove(string name)
    {
        var sources = GetAll();

        var source = sources.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        
        if (source is null)
        {
            throw new InvalidOperationException($"Source {name} does not exist.");
        }

        sources.Remove(source);
        
        Save(sources);
    }

    private void Save(List<SaveSource> sources)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        var json = JsonSerializer.Serialize(sources, options);
        
        File.WriteAllText(_filePath, json);
    }
}