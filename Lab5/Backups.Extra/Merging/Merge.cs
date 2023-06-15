using Backups.Exception;
using Backups.Extra.Data;
using Backups.InMemoryRepository;

namespace Backups.Extra.Merging;

public class Merge
{
    private RestorePoint _oldRestorePoint;
    private RestorePoint _newRestorePoint;

    public RestorePoint Merging(RestorePoint restorePoint1, RestorePoint restorePoint2)
    {
        if (restorePoint1 == null || restorePoint2 == null)
        {
            throw new BackupsException("Null reference of restore points");
        }

// checking for single storage
        if (restorePoint1.Storages.Count == 1)
        {
            if (restorePoint1.Storages.Any(storage => storage.Archive.BackupObjects.Count != 1))
            {
                return restorePoint1;
            }
        }

        if (restorePoint2.Storages.Count == 1)
        {
            if (restorePoint2.Storages.Any(storage => storage.Archive.BackupObjects.Count != 1))
            {
                return restorePoint2;
            }
        }

        if (restorePoint1.GetName() < restorePoint2.GetName())
        {
            _oldRestorePoint = restorePoint1;
            _newRestorePoint = restorePoint2;
        }
        else
        {
            _oldRestorePoint = restorePoint2;
            _newRestorePoint = restorePoint1;
        }

        List<Storage> newStorageList = new List<Storage>();
        foreach (var storage in _oldRestorePoint.Storages)
        {
            foreach (var backupObject in storage.BackupObjects)
            {
                if (!RepeatedBackupObject(backupObject))
                {
                    var backupObjects = new List<BackupObject>() { backupObject };
                    newStorageList.Add(storage);
                }
            }
        }

        foreach (var storage in _oldRestorePoint.Storages)
        {
            foreach (var backupObject in storage.BackupObjects)
            {
                if (RepeatedBackupObject(backupObject))
                {
                    var backupObjects = new List<BackupObject>() { backupObject };
                    newStorageList.Add(storage);
                }
            }
        }

        return new RestorePoint(_newRestorePoint.Name, _newRestorePoint.GetName(), newStorageList);
    }

    private bool RepeatedBackupObject(BackupObject backupObject)
    {
        bool newexist = false;
        bool oldexist = false;
        if (backupObject == null)
        {
            throw new BackupsException("Null reference of backup object");
        }

        foreach (var value in _newRestorePoint.Storages)
        {
            foreach (var valueBackup in value.BackupObjects)
            {
                if (valueBackup == backupObject)
                    newexist = true;
            }
        }

        foreach (var value in _oldRestorePoint.Storages)
        {
            foreach (var valueBackup in value.BackupObjects)
            {
                if (valueBackup == backupObject)
                    oldexist = true;
            }
        }

        return newexist && oldexist;
    }
}