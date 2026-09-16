using System.CommandLine;
using ZomVault.Core.SaveSource;

namespace ZomVault.Cli.Commands.Source;

public static class RemoveSourceCommand
{
    public static Command Create()
    {
        var sourceRepository = new SourceManifestRepository();
        
        var command = new Command(
            "remove",
            "Remove a save source"
        );

        var nameArgument = new Argument<string>("name")
        {
            Description = "The name of the source"
        };
        
        command.Arguments.Add(nameArgument);
        
        command.SetAction(parseResult =>
        {
            var name = parseResult.GetValue(nameArgument)!;

            try
            {
                sourceRepository.Remove(name);

                Console.WriteLine($"Source '{name}' was successfully removed");

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