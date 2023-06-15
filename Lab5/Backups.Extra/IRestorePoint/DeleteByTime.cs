using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.IRestorePoint;

public class DeleteByTime
{
    public IReadOnlyCollection<RestorePoint> DeleteRestorePoints(List<RestorePoint> restorePoints, DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue)
        {
            throw new BackupsException("Invalid Date");
        }

        foreach (var value in restorePoints.Where(value => value.GetName() < dateTime))
        {
            restorePoints.Remove(value);
        }

        return restorePoints;
    }
}