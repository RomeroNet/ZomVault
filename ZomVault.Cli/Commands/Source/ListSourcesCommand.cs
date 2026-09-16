using System.CommandLine;
using ZomVault.Core.SaveSource;

namespace ZomVault.Cli.Commands.Source;

public static class ListSourcesCommand
{
    public static Command Create()
    {
        var sourceRepository = new SourceManifestRepository();
        
        var command = new Command(
            "list",
            "List configured save sources"
        );
        
        command.SetAction(_ =>
        {
            var sources = sourceRepository.GetAll();

            if (sources.Count == 0)
            {
                Console.WriteLine("No sources configured");
                return 0;
            }

            foreach (var source in sources)
            {
                Console.WriteLine($"{source.Name}: {source.Path}");
            }

            return 0;
        });

        return command;
    }
}