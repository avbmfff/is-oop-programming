using Backups.Exception;
using Backups.LocalRepository.Tools;

namespace Backups.LocalRepository;

public class BackupTask
{
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();
    private List<Storage> _storages = new List<Storage>();
    private List<BackupObject> _backupObjects = new List<BackupObject>();
    private IAlgorithm _algorithm;

    public BackupTask(IAlgorithm algorithm)
    {
        _algorithm = algorithm ?? throw new BackupsException("Get a null argument");
    }

    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;
    public IReadOnlyCollection<BackupObject> BackupObjects => _backupObjects;
    public IReadOnlyCollection<Storage> Storages => _storages;
    public IAlgorithm Algorithm => _algorithm;
    public void AddBackup(BackupObject backupObject)
    {
        if (backupObject == null)
        {
            throw new BackupsException("Null data");
        }

        if (BackupObjects.Contains(backupObject))
        {
            throw new BackupsException("Invalid data");
        }

        _backupObjects.Add(backupObject);
    }

    public void RemoveBackup(BackupObject backupObject)
    {
        if (backupObject == null)
        {
            throw new BackupsException("Null data");
        }

        if (!BackupObjects.Contains(backupObject))
        {
            throw new BackupsException("Invalid data");
        }

        _backupObjects.Remove(backupObject);
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

    public void ClearListBackup()
    {
        _backupObjects.Clear();
    }

    public BackupObject CreateBackupObject(string filename, string path)
    {
        var newBackupObject = new BackupObject(filename, path);
        AddBackup(newBackupObject);
        return newBackupObject;
    }

    public RestorePoint CreateRestorePoint(string name, List<Storage> storages)
    {
        var restorepoint = new RestorePoint(name, storages);
        return restorepoint;
    }

    public void Save(string name, string path)
    {
        var repository = new LocalRepository.Repository(path);
        foreach (var storage in repository.Save(_backupObjects, _algorithm, name).Where(storage => !_storages.Contains(storage)))
        {
            if (storage == null) continue;
            storage.Archive!.Save(path + @"/" + $"{storage.Name}.zip");
            _storages.Add(storage);
        }

        AddRestorePoint(CreateRestorePoint(name, _storages));
    }
}