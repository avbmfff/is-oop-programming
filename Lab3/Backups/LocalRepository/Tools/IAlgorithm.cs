namespace Backups.LocalRepository.Tools;

public interface IAlgorithm
{
    List<Storage> ZipArchive(List<BackupObject> backupObjects, string path, string name);
}