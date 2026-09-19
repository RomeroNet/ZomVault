using System.CommandLine;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.UseCase;

namespace ZomVault.Cli.Commands.Backup;

public class RestoreBackupCommand(
    BackupRepository repository,
    RestoreBackupUseCase restoreBackup
)
{
    public Command Create()
    {
        var command = new Command(
            "restore",
            "Decompress and apply a backup"
        );

        var fileNameArgument = new Argument<string>("fileName")
        {
            Description = "The backup filename"
        };

        command.Arguments.Add(fileNameArgument);

        command.SetAction(parseResult =>
        {
            var fileName = parseResult.GetValue(fileNameArgument)!;

            var backup = repository.GetWhereFileName(fileName);

            restoreBackup.Restore(backup);

            Console.WriteLine("Backup restored successfully");
        });

        return command;
    }
}