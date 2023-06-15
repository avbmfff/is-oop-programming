using Backups.Exception;
using Backups.InMemoryRepository.Tools;

namespace Backups.InMemoryRepository;

public class BackupTask
{
    private Directory _backupTaskDir;
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();
    private List<Storage> _storages = new List<Storage>();
    private List<BackupObject> _backupObjects = new List<BackupObject>();
    private ITools _algorithm;

    public BackupTask(ITools algorithm, string name)
    {
        _algorithm = algorithm ?? throw new BackupsException("Get a null argument");
        _backupTaskDir = new Directory(name);
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<Storage> Storages => _storages;
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public ITools Algorithm => _algorithm;
    public Directory GetDirectory()
    {
        return _backupTaskDir;
    }

    public void AddBackupObject(BackupObject backupObject)
    {
        if (backupObject == null)
        {
            throw new BackupsException("Null reference of file");
        }

        if (Contains(backupObject.GetName()))
        {
            throw new BackupsException("BackupObject already exist");
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackupObject(BackupObject backupObject)
    {
        if (backupObject == null)
        {
            throw new BackupsException("Null reference of file");
        }

        if (!Contains(backupObject.GetName()))
        {
            throw new BackupsException("BackupObject doesn't exist");
        }

        _backupObjects.Remove(backupObject);
    }

    public bool Contains(string backupname)
    {
        return _backupObjects.Any(backupObject => backupObject.GetName() == backupname);
    }

    public void ClearListOfBackupObjects()
    {
        _backupObjects.Clear();
    }

    public void AddRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint == null)
        {
            throw new BackupsException("Null data");
        }

        if (_restorePoints.Contains(restorePoint))
        {
            throw new BackupsException("Invalid data");
        }

        _backupTaskDir.Add(restorePoint.GetRestorPointDirectory());
        _restorePoints.Add(restorePoint);
    }

    public void RemoveRestorePoint(RestorePoint restorePoint)
    {
        if (restorePoint == null)
        {
            throw new BackupsException("Null data");
        }

        if (!_restorePoints.Contains(restorePoint))
        {
            throw new BackupsException("Invalid data");
        }

        _restorePoints.Remove(restorePoint);
    }

    public RestorePoint CreateRestorePoint(string name, List<Storage> storages)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference if name");
        }

        if (storages == null)
        {
            throw new BackupsException("Null reference of argument");
        }

        var restorepoint = new RestorePoint(name, storages);
        return restorepoint;
    }

    public RestorePoint CreateRestorePoint(string name, List<Storage> storages, DateTime creatingTime)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference if name");
        }

        if (storages == null)
        {
            throw new BackupsException("Null reference of argument");
        }

        var restorepoint = new RestorePoint(name, creatingTime, storages);
        return restorepoint;
    }

    public void Save(Directory directory, string restorePointName, DateTime creatingTime)
    {
        if (string.IsNullOrWhiteSpace(restorePointName))
        {
            throw new BackupsException("Null reference od Restore Point's name");
        }

        if (directory == null)
        {
            throw new BackupsException("Null reference of directory");
        }

        var storages = new List<Storage>(directory.Save(_backupObjects, _algorithm));
        AddRestorePoint(CreateRestorePoint(restorePointName, storages, creatingTime));
        foreach (Storage storage in storages)
        {
            _storages.Add(storage);
        }
    }

    public void Save(Directory directory, string restorePointName)
    {
        if (string.IsNullOrWhiteSpace(restorePointName))
        {
            throw new BackupsException("Null reference od Restore Point's name");
        }

        var storages = new List<Storage>(directory.Save(_backupObjects, _algorithm));
        AddRestorePoint(CreateRestorePoint(restorePointName, storages));
        foreach (Storage storage in storages)
        {
            _storages.Add(storage);
        }
    }
}