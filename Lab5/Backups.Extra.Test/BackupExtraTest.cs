using Backups.InMemoryRepository;
using Xunit;
using Directory = Backups.InMemoryRepository.Directory;
using File = Backups.InMemoryRepository.File;

namespace Backups.Extra.Test;

public class BackupExtraTest
{
    [Fact]
    public void CheckDeletingRestorePoints()
    {
        Directory directory = new Directory("die");
        BackupObject backupObject = new BackupObject(directory);
        List<BackupObject> backupObjects = new List<BackupObject>() { backupObject };
        Storage storage = new Storage("dkdkd", backupObjects);
        List<Storage> storages = new List<Storage>() { storage };
        RestorePoint restorePoint1 = new RestorePoint("dfdf", new DateTime(2020, 01, 23), storages);
        RestorePoint restorePoint2 = new RestorePoint("ffdf", new DateTime(2021, 01, 23), storages);
        List<RestorePoint> restorePoints = new List<RestorePoint>() { restorePoint1, restorePoint2 };
        IBackup ibackup = new Backup();
        var newList = ibackup.DeleteRestorePointForCount(restorePoints, 1);
        Assert.True(newList.Count == 1);
    }

    [Fact]
    public void CheckRestorePointsMerge()
    {
        Directory directory = new Directory("die");
        BackupObject backupObject = new BackupObject(directory);
        List<BackupObject> backupObjects = new List<BackupObject>() { backupObject };
        Storage storage = new Storage("dkdkd", backupObjects);
        List<Storage> storages = new List<Storage>() { storage };
        RestorePoint restorePoint1 = new RestorePoint("dfdf", new DateTime(2020, 01, 23), storages);
        RestorePoint restorePoint2 = new RestorePoint("ffdf", new DateTime(2021, 01, 23), storages);
        List<RestorePoint> restorePoints = new List<RestorePoint>() { restorePoint1, restorePoint2 };
        IBackup ibackup = new Backup();
        ibackup.UpdateRestorePointData(restorePoint1);
        ibackup.UpdateRestorePointData(restorePoint2);
        ibackup.Merging(restorePoint1, restorePoint2);
        Assert.True(ibackup.GetRestorePointData().Count == 1);
    }

    [Fact]
    public void CheckRestorePointRecovering()
    {
        Directory directory = new Directory("die");
        BackupObject backupObject = new BackupObject(directory);
        List<BackupObject> backupObjects = new List<BackupObject>() { backupObject };
        Storage storage = new Storage("dkdkd", backupObjects);
        List<Storage> storages = new List<Storage>() { storage };
        RestorePoint restorePoint1 = new RestorePoint("dfdf", new DateTime(2020, 01, 23), storages);
        List<RestorePoint> restorePoints = new List<RestorePoint>() { restorePoint1 };
        IBackup ibackup = new Backup();
        ibackup.UpdateRestorePointData(restorePoint1);
        var difdir = new Directory("123456");
        ibackup.RecoveringIntoDifferent(restorePoint1, difdir);
        Assert.True(difdir.Components.Count == 1);
    }
}