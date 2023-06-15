using Backups.Exception;
using Backups.InMemoryRepository.Tools;

namespace Backups.InMemoryRepository;

public class RepositoryComponent
{
    private string name;
    public RepositoryComponent(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new BackupsException("Null reference of name");
        }

        this.name = name;
        Parent = null;
    }

    public RepositoryComponent Parent { get; protected internal set; }
    public virtual void Add(RepositoryComponent component)
    {
        if (component == null)
        {
            throw new BackupsException("Null reference of component");
        }

        component.Parent = this;
    }

    public virtual string GetName()
    {
        return name;
    }

    public virtual void Remove(RepositoryComponent component)
    {
        if (component == null)
        {
            throw new BackupsException("Null reference of component");
        }

        component.Parent = null;
    }

    public virtual IReadOnlyCollection<RepositoryComponent> GetComponents()
    {
        return null;
    }

    public IReadOnlyCollection<Storage> Save(List<BackupObject> backupObjects, ITools algorithm)
    {
        if (backupObjects == null)
        {
            throw new BackupsException("Null reference of list");
        }

        List<Storage> storages = new List<Storage>(algorithm.ZipArchive(backupObjects, name));

        return storages;
    }
}