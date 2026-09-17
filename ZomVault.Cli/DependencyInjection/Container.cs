using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using ZomVault.Cli.Commands.Backup;
using ZomVault.Cli.Commands.Source;
using ZomVault.Core.Backup;
using ZomVault.Core.Backup.UseCase;
using ZomVault.Core.Database;
using ZomVault.Core.SaveSource;

namespace ZomVault.Cli.DependencyInjection;

public class Container
{
    private readonly ServiceCollection _services = new();

    public Container()
    {
        RegisterServices();
    }

    private void RegisterServices()
    {
        _services.AddDbContext<ZomVaultDatabaseContext>();
        
        _services.AddScoped<CreateBackupUseCase>();

        _services.AddScoped<SourceRepository>();
        _services.AddScoped<BackupRepository>();

        _services.AddScoped<AddSourceCommand>();
        _services.AddScoped<ListSourcesCommand>();
        _services.AddScoped<RemoveSourceCommand>();
        _services.AddScoped<SourceCommand>();

        _services.AddScoped<CreateBackupCommand>();
        _services.AddScoped<ListBackupsCommand>();
        _services.AddScoped<BackupCommand>();
    }

    public ServiceProvider Build()
    {
        return _services.BuildServiceProvider();
    }
}