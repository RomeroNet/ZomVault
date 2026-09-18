using System.CommandLine;
using ZomVault.Core.SaveSource;

namespace ZomVault.Cli.Commands.Source;

public class AddSourceCommand(SourceRepository sourceRepository)
{
    public Command Create()
    {
        var command = new Command(
            "add",
            "Add a new save source"
        );

        var nameArgument = new Argument<string>("name")
        {
            Description = "The name of the source"
        };

        var pathArgument = new Argument<string>("path")
        {
            Description = "The path of the saved game"
        };
        
        command.Arguments.Add(nameArgument);
        command.Arguments.Add(pathArgument);
        
        command.SetAction(parseResult =>
        {
            var name = parseResult.GetValue(nameArgument)!;
            var path = parseResult.GetValue(pathArgument)!;

            if (!Directory.Exists(path))
            {
                Console.Error.WriteLine($"Directory {path} does not exist");

                return 1;
            }

            try
            {
                var source = new SaveSourceModel()
                {
                    Name = name,
                    Path = Path.GetFullPath(path)
                };

                sourceRepository.Add(source);

                Console.WriteLine($"Source '{name}' was successfully added");

                return 0;
            }
            catch (InvalidOperationException e)
            {
                Console.Error.WriteLine(e.Message);

                return 1;
            }
        });

        return command;
    }
}