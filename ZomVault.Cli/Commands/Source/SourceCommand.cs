using System.CommandLine;

namespace ZomVault.Cli.Commands.Source;

public class SourceCommand(
    AddSourceCommand addSourceCommand,
    ListSourcesCommand listSourcesCommand,
    RemoveSourceCommand removeSourceCommand
) {
    public Command Create()
    {
        var sourceCommand = new Command(
            "source",
            "Manage save sources"
        );

        sourceCommand.Add(addSourceCommand.Create());
        sourceCommand.Add(listSourcesCommand.Create());
        sourceCommand.Add(removeSourceCommand.Create());

        return sourceCommand;
    }
}