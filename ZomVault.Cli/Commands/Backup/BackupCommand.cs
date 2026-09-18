using System.CommandLine;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.UseCase;
using ZomVault.Core.SaveSource;

namespace ZomVault.Cli.Commands.Backup;

public class BackupCommand(
    CreateBackupCommand createBackupCommand,
    ListBackupsCommand listBackupsCommand,
    RemoveBackupCommand removeBackupCommand
) {
    public Command Create()
    {
        var backupCommand = new Command(
            "backup",
            "Manage save backups"
        );

        backupCommand.Add(createBackupCommand.Create());
        backupCommand.Add(listBackupsCommand.Create());
        backupCommand.Add(removeBackupCommand.Create());
        
        return backupCommand;
    }
}