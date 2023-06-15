using Backups.Exception;

namespace Backups.LocalRepository;

public class RestorePoint
{
    private DateTime _creatingTime;
    private List<Storage> _storages;

    public RestorePoint(string name, List<Storage> storages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Invalid Restore Point's name");
        }

        Name = name;
        _creatingTime = DateTime.Now;
        _storages = storages;
    }

    public RestorePoint(string name, DateTime creatingTime, List<Storage> storages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Invalid Restore Point's name");
        }

        Name = name;
        _creatingTime = creatingTime;
        _storages = storages;
    }

    public string Name { get; }
    public IReadOnlyCollection<Storage> Storages => _storages;

    public DateTime GetName()
    {
        return _creatingTime;
    }

    public void AddStorage(Storage storage)
    {
        if (storage == null)
        {
            throw new BackupsException("Null Reference of backup object");
        }

        if (_storages.Contains(storage))
        {
            throw new BackupsException("Backup object doesn't exist");
        }

        _storages.Add(storage);
    }

    public void RemoveStorage(Storage storage)
    {
        if (storage == null && !_storages.Contains(storage))
        {
            throw new BackupsException("Argument can't exist");
        }

        _storages.Remove(storage);
    }

    public List<Storage> ReturnBackupObjects()
    {
        return _storages;
    }

    public bool ArgumentExist(Storage storage)
    {
        if (storage == null!)
        {
            throw new BackupsException("Null Argument");
        }

        return _storages.Contains(storage);
    }
}