using System.CommandLine;
using ZomVault.Core.Backup;

namespace ZomVault.Cli.Commands.Backup;

public class ListBackupsCommand(BackupRepository backupRepository)
{
    public Command Create()
    {
        var command = new Command(
            "list",
            "List available backups"
        );

        var sourceArgument = new Argument<string>("source")
        {
            Description = "The name of the backup source"
        };
        
        command.Arguments.Add(sourceArgument);
        
        command.SetAction(parseResult =>
        {
            var sourceName = parseResult.GetValue(sourceArgument)!;
            
            var backups = backupRepository.GetWhereSourceName(sourceName);

            if (backups.Count == 0)
            {
                throw new InvalidOperationException(
                    $"Source {sourceName} does not exists or does not have any backups");
            }

            Console.WriteLine($"Backups for {sourceName}:");
            foreach (var backup in backups)
            {
                Console.WriteLine($"- {backup.Filename}, created at {backup.CreatedAt.ToLocalTime()}");
            }

            return 0;
        });
        
        return command;
    }
}