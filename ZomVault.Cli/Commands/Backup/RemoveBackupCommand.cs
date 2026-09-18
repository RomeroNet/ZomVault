using System.CommandLine;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.UseCase;

namespace ZomVault.Cli.Commands.Backup;

public class RemoveBackupCommand(
    BackupRepository repository,
    DeleteBackupUseCase deleteBackup
) {
    public Command Create()
    {
        var command = new Command(
            "remove",
            "Delete a backup"
        );

        var fileNameArgument = new Argument<string>("filename")
        {
            Description = "The backup filename"
        };
        
        command.Arguments.Add(fileNameArgument);
        
        command.SetAction(parseResult =>
        {
            var fileName = parseResult.GetValue(fileNameArgument)!;
            
            var backup = repository.GetWhereFileName(fileName);
            
            deleteBackup.Delete(backup);
            
            Console.WriteLine("Backup deleted successfully");
        });

        return command;
    }
}