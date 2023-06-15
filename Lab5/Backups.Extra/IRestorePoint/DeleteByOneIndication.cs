using Backups.Exception;
using Backups.InMemoryRepository;

namespace Backups.Extra.IRestorePoint;

public class DeleteByOneIndication
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

        for (int i = 0; i < restorePoints.Count - 7; i++)
        {
            if (restorePoints[i].GetName() < dateTime)
            {
                restorePoints.Remove(restorePoints[i]);
            }
        }

        return restorePoints;
    }
}