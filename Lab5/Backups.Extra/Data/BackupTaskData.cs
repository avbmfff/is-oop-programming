using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.Data;

public class BackupTaskData
{
    private List<BackupTask> _backupTasks = new List<BackupTask>();
    public IReadOnlyCollection<BackupTask> BackupTasks => _backupTasks;

    public void AddBackupTask(BackupTask backupTask)
    {
        if (backupTask == null || Contains(backupTask))
        {
            throw new BackupsException("BackupTask doesn't exist");
        }

        _backupTasks.Add(backupTask);
    }

    public void RemoveBackupTask(BackupTask backupTask)
    {
        if (backupTask == null || !Contains(backupTask))
        {
            throw new BackupsException("BackupTask already exist");
        }

        _backupTasks.Remove(backupTask);
    }

    private bool Contains(BackupTask backupTask)
    {
        return _backupTasks.Any(value => backupTask.GetDirectory() == value.GetDirectory());
    }
}