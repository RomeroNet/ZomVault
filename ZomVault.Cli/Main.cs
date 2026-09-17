using System.CommandLine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ZomVault.Cli.Commands.Backup;
using ZomVault.Cli.Commands.Source;
using ZomVault.Core.Database;
using Container = ZomVault.Cli.DependencyInjection.Container;

var container = new Container();

using var provider = container.Build();

using var db = provider.GetRequiredService <ZomVaultDatabaseContext>();

db.Database.Migrate();

var rootCommand = new RootCommand("Project Zomboid save backup manager");

var sourceCommand = provider.GetRequiredService <SourceCommand>();
var backupCommand = provider.GetRequiredService<BackupCommand>();

rootCommand.Add(sourceCommand.Create());
rootCommand.Add(backupCommand.Create());

return await rootCommand.Parse(args).InvokeAsync();
