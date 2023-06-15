using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.IRestorePoint;

public class DeleteByQuantity
{
    private const int MinValue = 0;
    public IReadOnlyCollection<RestorePoint> DeleteRestorePoints(List<RestorePoint> restorePoints, int amount)
    {
        if (amount < MinValue || amount > restorePoints.Count)
        {
            throw new BackupsException("Invalid amount");
        }

        restorePoints.Reverse();
        var newRestorePoints = new List<RestorePoint>();
        while (amount != MinValue)
        {
            newRestorePoints.Add(restorePoints[0]);
            restorePoints.Remove(restorePoints[0]);
            amount--;
        }

        return newRestorePoints;
    }
}