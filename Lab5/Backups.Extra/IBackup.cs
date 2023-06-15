using Backups.Extra.Data;
using Backups.Extra.Logging;
using Backups.Extra.Recovering;
using Backups.InMemoryRepository;
using Backups.InMemoryRepository.Tools;
using Directory = Backups.InMemoryRepository.Directory;

namespace Backups.Extra;

public interface IBackup
{
    BackupTask CreateBackupTask(ITools tools, string name);
    IReadOnlyCollection<RestorePoint> CreateRestorePoint(BackupTask backupTask, string name, DateTime dateTime, Directory directory, IReceiver receiver);
    IReadOnlyCollection<BackupTask> GetBackupTaskData();
    IReadOnlyCollection<RestorePoint> GetRestorePointData();
    void UpdateBackupTasksData(BackupTask backupTask);
    void UpdateRestorePointData();
    void UpdateRestorePointData(RestorePoint restorePoint);
    void Merging(RestorePoint restorePoint1, RestorePoint restorePoint2);
    void RecoveringIntoOriginal(RestorePoint restorePoint);
    void RecoveringIntoDifferent(RestorePoint restorePoint, Directory directory);
    IReadOnlyCollection<RestorePoint> DeleteRestorePointForCount(List<RestorePoint> restorePoints, int amount);
    IReadOnlyCollection<RestorePoint> DeleteRestorePointForDays(List<RestorePoint> restorePoints, DateTime dateTime);
    IReadOnlyCollection<RestorePoint> DeleteRestorePointForSingleHybrid(List<RestorePoint> restorePoints, DateTime dateTime, int amount);
    IReadOnlyCollection<RestorePoint> DeleteRestorePointForAllHybrid(List<RestorePoint> restorePoints, DateTime dateTime, int amount);
}