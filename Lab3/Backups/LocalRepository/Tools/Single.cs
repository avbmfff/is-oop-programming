using Ionic.Zip;

namespace Backups.LocalRepository.Tools;

public class AlgorithmSingle : IAlgorithm
{
    public List<Storage> ZipArchive(List<BackupObject> backupObjects, string path, string name)
    {
        var storages = new List<Storage>();
        var archive = new ZipFile();
        foreach (var value in backupObjects)
        {
            archive.AddItem(value.Path);
        }

        var storage = new Storage(archive, name);
        storages.Add(storage);
        return storages;
    }
}