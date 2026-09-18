namespace ZomVault.Core.Archive;

public interface IArchiver
{
    Stream Create(SaveSource.SaveSourceModel source);
}