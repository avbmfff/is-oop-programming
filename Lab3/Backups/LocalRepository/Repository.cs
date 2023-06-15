using Backups.Exception;
using Backups.LocalRepository.Tools;

namespace Backups.LocalRepository;

public class Repository
{
    private string _path;
    public Repository(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new BackupsException("Invalid path");
        }

        _path = path;
    }

    public List<Storage> Save(List<BackupObject> backupObjects, IAlgorithm algorithm, string name)
    {
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }

        var storages = algorithm.ZipArchive(backupObjects, _path, name);

        return storages;
    }
}