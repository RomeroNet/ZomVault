using System.CommandLine;

namespace ZomVault.Cli.Commands.Source;

public static class SourceCommand
{
    public static Command Create()
    {
        var sourceCommand = new Command(
            "source",
            "Manage save sources"
        );

        sourceCommand.Add(AddSourceCommand.Create());
        sourceCommand.Add(ListSourcesCommand.Create());
        sourceCommand.Add(RemoveSourceCommand.Create());

        return sourceCommand;
    }
}