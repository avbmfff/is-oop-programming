using Backups.Exception;

namespace Backups.InMemoryRepository;

public class RestorePoint
{
    private DateTime _creatingTime;
    private Directory _restorepointDir;
    private List<Storage> _storages;

    public RestorePoint(string name, List<Storage> storages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Invalid Restore Point's name");
        }

        if (storages == null)
        {
            throw new BackupsException("Null reference of storages list");
        }

        Name = name;
        _creatingTime = DateTime.Now;
        _storages = storages;
        _restorepointDir = new Directory(name);
        foreach (var storage in storages)
        {
            _restorepointDir.Add(storage.Archive);
        }
    }

    public RestorePoint(string name, DateTime creatingTime, List<Storage> storages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Invalid Restore Point's name");
        }

        if (storages == null)
        {
            throw new BackupsException("Null reference of storages list");
        }

        Name = name;
        _creatingTime = creatingTime;
        _storages = storages;
        _restorepointDir = new Directory(name);
        foreach (var storage in storages)
        {
            _restorepointDir.Add(storage.Archive);
        }
    }

    public string Name { get; }
    public IReadOnlyCollection<Storage> Storages => _storages;

    public RepositoryComponent GetRestorPointDirectory()
    {
        return _restorepointDir;
    }

    public IReadOnlyCollection<RepositoryComponent> GetRestorPointDirectoryComponents()
    {
        return _restorepointDir.Components;
    }

    public DateTime GetName()
    {
        return _creatingTime;
    }

    public void AddStorage(Storage storage)
    {
        if (storage == null)
        {
            throw new BackupsException("Null Reference of storage");
        }

        if (Contains(storage.Name))
        {
            throw new BackupsException("Storage doesn't exist");
        }

        _storages.Add(storage);
    }

    public void AddListStorage(List<Storage> storages)
    {
        if (storages == null)
        {
            throw new BackupsException("Null Reference of List of storages");
        }

        foreach (var storage in storages.Where(storage => !Contains(storage.Name)))
        {
            _storages.Add(storage);
        }
    }

    public void RemoveStorage(Storage storage)
    {
        if (storage == null && !Contains(storage.Name))
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

    public bool Contains(string storageName)
    {
        return _storages.Any(storage => storage.Name == storageName);
    }
}