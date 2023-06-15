using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.Data;

public class RestorePointData
{
    private List<RestorePoint> _restorePoints = new List<RestorePoint>();
    public IReadOnlyCollection<RestorePoint> RestorePoints => _restorePoints;

    public void Add(BackupTaskData backupTaskData)
    {
        if (backupTaskData == null)
        {
            throw new BackupsException("list of backup tasks doesn't exist");
        }

        foreach (var value in backupTaskData.BackupTasks)
        {
            foreach (var restorePoint in value.RestorePoints)
            {
                if (!Contains(restorePoint))
                {
                    _restorePoints.Add(restorePoint);
                }
            }
        }
    }

    public void Add(RestorePoint restorePoint)
    {
        if (restorePoint == null)
            throw new BackupsException("null reference of restore point");
        _restorePoints.Add(restorePoint);
    }

    public void Remove(RestorePoint restorePoint)
    {
        if (!Contains(restorePoint) || restorePoint == null)
        {
            throw new BackupsException("restore point doesn't exist or already exist");
        }

        _restorePoints.Remove(restorePoint);
    }

    public bool Contains(RestorePoint restorePoint)
    {
        return _restorePoints.Any(value => value.Name == restorePoint.Name);
    }
}