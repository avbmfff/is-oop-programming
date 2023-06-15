using Backups.Exception;

namespace Backups.LocalRepository;

public class BackupObject
{
    public BackupObject(string filename, string path)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            throw new BackupsException("Null reference of file name");
        }

        if (string.IsNullOrEmpty(path))
        {
            throw new BackupsException("Null reference of path");
        }

        FileName = filename;
        Path = path;
    }

    public string FileName { get; }
    public string Path { get; }
}