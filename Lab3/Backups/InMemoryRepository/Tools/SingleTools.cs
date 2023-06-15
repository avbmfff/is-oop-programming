using Backups.Exception;

namespace Backups.InMemoryRepository.Tools;

public class SingleTools : ITools
{
    public IReadOnlyCollection<Storage> ZipArchive(List<BackupObject> backupObjects, string name)
    {
        if (backupObjects == null)
        {
            throw new BackupsException("Null reference of list");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference of storage name");
        }

        var storages = new List<Storage>();
        var storage = new Storage(name, backupObjects);
        storages.Add(storage);
        return storages;
    }
}