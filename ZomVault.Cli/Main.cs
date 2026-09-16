using System.CommandLine;
using ZomVault.Cli.Commands.Backup;
using ZomVault.Cli.Commands.Source;

var rootCommand = new RootCommand("Project Zomboid save backup manager");

rootCommand.Add(SourceCommand.Create());
rootCommand.Add(BackupCommand.Create());

return await rootCommand.Parse(args).InvokeAsync();
