using System.CommandLine;
using ZomVault.Core.Backup.UseCase;
using ZomVault.Core.SaveSource;

namespace ZomVault.Cli.Commands.Backup;

public class CreateBackupCommand(
    SourceRepository sourceRepository,
    CreateBackupUseCase createBackupUseCase
) {
    public Command Create()
    {
        var command = new Command(
            "create",
            "Generate a backup from a source"
        );

        var sourceArgument = new Argument<string>("source")
        {
            Description = "The source from which the backup will be generated"
        };
        
        command.Arguments.Add(sourceArgument);
        
        command.SetAction(parseResult =>
        {
            var sourceName = parseResult.GetValue(sourceArgument)!;

            var sources = sourceRepository.GetAll();

            var source = sources.FirstOrDefault(
                x => x.Name.Equals(sourceName, StringComparison.OrdinalIgnoreCase)
            );

            if (source is null)
            {
                throw new InvalidOperationException($"Source {sourceName} does not exist.");
            }

            createBackupUseCase.Create(source);
            
            Console.WriteLine($"Backup written to: 'WIP: Add this'");
        });

        return command;
    }
}