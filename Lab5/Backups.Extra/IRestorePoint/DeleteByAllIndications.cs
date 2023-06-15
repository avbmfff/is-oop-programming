using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.IRestorePoint;

public class DeleteByAllIndications
{
    private const int MinValue = 0;

    public IReadOnlyCollection<RestorePoint> DeleteRestorePoints(List<RestorePoint> restorePoints, int amount, DateTime dateTime)
    {
        if (amount < MinValue || amount > restorePoints.Count)
        {
            throw new BackupsException("Invalid amount");
        }

        if (dateTime == DateTime.MinValue)
        {
            throw new BackupsException("Invalid Date");
        }

        restorePoints.Reverse();
        var newRestorePoints = new List<RestorePoint>();
        while (amount != MinValue)
        {
            newRestorePoints.Add(restorePoints[0]);
            restorePoints.Remove(restorePoints[0]);
            amount--;
        }

        foreach (RestorePoint value in newRestorePoints.Where(value => value.GetName() < dateTime))
        {
            newRestorePoints.Remove(value);
        }

        return newRestorePoints;
    }
}