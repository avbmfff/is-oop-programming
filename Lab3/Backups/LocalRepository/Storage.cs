using Backups.Exception;
using Ionic.Zip;

namespace Backups.LocalRepository;

public class Storage
{
    public Storage(ZipFile archive, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Invalid stream argument");
        }

        Name = name;
        Archive = archive;
    }

    public string Name { get; }
    public ZipFile Archive { get; }
}