using Backups.Exception;
using Backups.Extra.Data;
using Backups.Extra.IRestorePoint;
using Backups.Extra.Logging;
using Backups.Extra.Merging;
using Backups.Extra.Recovering;
using Backups.InMemoryRepository;
using Backups.InMemoryRepository.Tools;
using Directory = Backups.InMemoryRepository.Directory;

namespace Backups.Extra;

public class Backup : IBackup
{
    private BackupTaskData _backupTaskData = new BackupTaskData();
    private RestorePointData _restorePointData = new RestorePointData();

    public BackupTask CreateBackupTask(ITools tools, string name)
    {
        if (tools == null || string.IsNullOrWhiteSpace(name))
            throw new BackupsException("Null reference of data");
        return new BackupTask(tools, name);
    }

    public IReadOnlyCollection<RestorePoint> CreateRestorePoint(BackupTask backupTask, string name, DateTime dateTime, Directory directory, IReceiver receiver)
    {
        if (backupTask == null || string.IsNullOrWhiteSpace(name) || dateTime == DateTime.MinValue)
            throw new BackupsException("Null reference of data");
        if (directory == null || receiver == null)
            throw new BackupsException("Null reference of data");
        backupTask.Save(directory, name, dateTime);
        CreateLogForRestorePoint(receiver, backupTask.RestorePoints);
        return backupTask.RestorePoints;
    }

    public IReadOnlyCollection<BackupTask> GetBackupTaskData()
    {
        return _backupTaskData.BackupTasks;
    }

    public IReadOnlyCollection<RestorePoint> GetRestorePointData()
    {
        return _restorePointData.RestorePoints;
    }

    public void UpdateBackupTasksData(BackupTask backupTask)
    {
        if (backupTask == null)
            throw new BackupsException("Null reference of backup task");
        _backupTaskData.AddBackupTask(backupTask);
    }

    public void UpdateRestorePointData()
    {
        _restorePointData.Add(_backupTaskData);
    }

    public void UpdateRestorePointData(RestorePoint restorePoint)
    {
        if (restorePoint == null)
            throw new BackupsException("Null reference of restore point");
        _restorePointData.Add(restorePoint);
    }

    public void Merging(RestorePoint restorePoint1, RestorePoint restorePoint2)
    {
        if (restorePoint1 == null || restorePoint2 == null)
            throw new BackupsException("null reference of restore point");
        _restorePointData.Add(new Merge().Merging(restorePoint1, restorePoint2));
        _restorePointData.Remove(restorePoint1);
        _restorePointData.Remove(restorePoint2);
    }

    public void RecoveringIntoOriginal(RestorePoint restorePoint)
    {
        if (restorePoint == null)
            throw new BackupsException("null reference of restore point");
        new RecoverToOriginal().Recover(restorePoint);
    }

    public void RecoveringIntoDifferent(RestorePoint restorePoint, Directory directory)
    {
        if (restorePoint == null)
            throw new BackupsException("null reference of restore point");
        new RecoverToDifferent().Recover(restorePoint, directory);
    }

    public IReadOnlyCollection<RestorePoint> DeleteRestorePointForCount(List<RestorePoint> restorePoints, int amount)
    {
        if (restorePoints == null)
            throw new BackupsException("null reference of restore points list");
        if (amount < 0)
            throw new BackupsException("invalid degree of amount");
        return new DeleteByQuantity().DeleteRestorePoints(restorePoints, amount);
    }

    public IReadOnlyCollection<RestorePoint> DeleteRestorePointForDays(List<RestorePoint> restorePoints, DateTime dateTime)
    {
        if (restorePoints == null)
            throw new BackupsException("Null reference of restore points list");
        if (dateTime == DateTime.MinValue)
            throw new BackupsException("invalid degree of datetime");
        return new DeleteByTime().DeleteRestorePoints(restorePoints, dateTime);
    }

    public IReadOnlyCollection<RestorePoint> DeleteRestorePointForSingleHybrid(List<RestorePoint> restorePoints, DateTime dateTime, int amount)
    {
        if (restorePoints == null)
            throw new BackupsException("null reference of restore points list");
        if (dateTime == DateTime.MinValue)
            throw new BackupsException("invalid degree of datetime");
        if (amount < 0)
            throw new BackupsException("invalid degree of amount");
        return new DeleteByOneIndication().DeleteRestorePoints(restorePoints, amount, dateTime);
    }

    public IReadOnlyCollection<RestorePoint> DeleteRestorePointForAllHybrid(List<RestorePoint> restorePoints, DateTime dateTime, int amount)
    {
        if (restorePoints == null)
            throw new BackupsException("null reference of restore points list");
        if (dateTime == DateTime.MinValue)
            throw new BackupsException("invalid degree of datetime");
        if (amount < 0)
            throw new BackupsException("invalid degree of amount");
        return new DeleteByAllIndications().DeleteRestorePoints(restorePoints, amount, dateTime);
    }

    private void CreateLogForRestorePoint(IReceiver receiver, IReadOnlyCollection<RestorePoint> restorePoints)
    {
        if (receiver == null || restorePoints == null)
            throw new BackupsException("Null reference of data");
        var logger = new Logger();
        foreach (RestorePoint restorePoint in restorePoints)
        {
            string message = "Creating a " + typeof(RestorePoint) + " " + restorePoint.Name + " at " + restorePoint.GetName();
            logger.SetReceiver(receiver);
            logger.Execute(message);
            CreateLogForStorage(receiver, restorePoint);

            // getname возвращает время создания, а Name имя restorepoint
        }
    }

    private void CreateLogForStorage(IReceiver receiver, RestorePoint restorePoint)
    {
        if (receiver == null || restorePoint == null)
            throw new BackupsException("Null reference of data");
        var logger = new Logger();
        foreach (Storage storage in restorePoint.Storages)
        {
            string message = "Creating a " + typeof(Storage) + " " + storage.Name + " at " + restorePoint.GetName();
            logger.SetReceiver(receiver);
            logger.Execute(message);
        }
    }
}