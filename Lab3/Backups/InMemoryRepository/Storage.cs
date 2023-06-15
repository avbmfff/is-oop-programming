using Backups.Exception;

namespace Backups.InMemoryRepository;

public class Storage
{
    private List<BackupObject> _backupObjects;
    public Storage(string name, List<BackupObject> backupObjects)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference of storage name");
        }

        if (backupObjects == null)
        {
            throw new BackupsException("Invalid stream argument");
        }

        Name = name;
        _backupObjects = backupObjects;
        Archive = new Archive(name, backupObjects);
    }

    public Storage(string name, BackupObject backupObjects)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference of storage name");
        }

        if (backupObjects == null)
        {
            throw new BackupsException("Invalid stream argument");
        }

        Name = name;
        Archive = new Archive(name, backupObjects);
        _backupObjects.Add(backupObjects);
    }

    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public string Name { get; }
    public Archive Archive { get; }

    public IReadOnlyCollection<BackupObject> ReturnListBackupObjects()
    {
        return BackupObjects;
    }
}