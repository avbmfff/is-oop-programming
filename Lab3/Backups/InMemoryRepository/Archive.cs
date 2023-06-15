using Backups.Exception;

namespace Backups.InMemoryRepository;

public class Archive : RepositoryComponent
{
    private List<BackupObject> _backupObjects;
    public Archive(string name, List<BackupObject> backupObjects)
        : base(name)
    {
        _backupObjects = backupObjects;
    }

    public Archive(string name, BackupObject backupObject)
        : base(name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference of argument");
        }

        if (backupObject == null)
        {
            throw new BackupsException("Null reference of argument");
        }

        _backupObjects.Add(backupObject);
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
}