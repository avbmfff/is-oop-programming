namespace Backups.InMemoryRepository.Tools;

public interface ITools
{
    IReadOnlyCollection<Storage> ZipArchive(List<BackupObject> backupObjects, string name);
}