using Backups.Exception;
using Backups.InMemoryRepository;
using Directory = Backups.InMemoryRepository.Directory;

namespace Backups.Extra.Recovering;

public class RecoverToDifferent
{
    public void Recover(RestorePoint restorePoint, Directory directory)
    {
        if (restorePoint == null || directory == null)
        {
            throw new BackupsException("Null reference of data");
        }

        foreach (var storage in restorePoint.Storages)
        {
            foreach (var backupObject in storage.BackupObjects)
            {
                directory.Add(backupObject.RepositoryComponent);
            }
        }
    }
}