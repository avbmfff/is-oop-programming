using Backups.Exception;
using Directory = Backups.InMemoryRepository.Directory;
using File = Backups.InMemoryRepository.File;

namespace Backups.InMemoryRepository;

public class BackupObject
{
    private string _name;

    public BackupObject(RepositoryComponent repositoryComponent)
    {
        if (repositoryComponent == null)
        {
            throw new BackupsException("Null reference of directory");
        }

        RepositoryComponent = repositoryComponent;
        _name = repositoryComponent.GetName();
    }

    public RepositoryComponent RepositoryComponent { get; protected set; }

    public string GetName()
    {
        return _name;
    }
}