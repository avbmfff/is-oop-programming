using Ionic.Zip;

namespace Backups.LocalRepository.Tools;

public class AlgorithmSplit : IAlgorithm
{
    public List<Storage> ZipArchive(List<BackupObject> backupObjects, string path, string name)
    {
        var storages = new List<Storage>();

        foreach (BackupObject value in backupObjects)
        {
            var archive = new ZipFile();
            archive.AddItem(value.Path);
            var storage = new Storage(archive, name);
            storages.Add(storage);
        }

        return storages;
    }
}