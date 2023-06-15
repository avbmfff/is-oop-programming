using Backups.Exception;
using Backups.InMemoryRepository;
using Directory = Backups.InMemoryRepository.Directory;

namespace Backups.Extra.Recovering;

public class RecoverToOriginal
{
    public void Recover(RestorePoint restorePoint)
    {
        if (restorePoint == null)
        {
            throw new BackupsException("Null reference of data");
        }

        foreach (var storage in restorePoint.Storages)
        {
            foreach (var backupObject in storage.BackupObjects)
            {
                if (ExistFile(backupObject))
                {
                    Replacement(backupObject);
                }
                else
                {
                    backupObject.RepositoryComponent.Parent.Add(backupObject.RepositoryComponent);
                }
            }
        }
    }

    private bool ExistFile(BackupObject backupObject)
    {
        return backupObject.RepositoryComponent.Parent.GetComponents().Any(value => backupObject.RepositoryComponent == value);
    }

    private void Replacement(BackupObject backupObject)
    {
        backupObject.RepositoryComponent.Parent.Add(backupObject.RepositoryComponent);
    }
}