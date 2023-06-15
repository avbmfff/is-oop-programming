using Backups.Exception;

namespace Backups.InMemoryRepository.Tools;

public class SplitTools : ITools
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

        return backupObjects.Select(backupObject => new Storage(name, backupObjects)).ToList();
    }
}