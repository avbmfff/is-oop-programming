using Backups.Exception;
using Backups.InMemoryRepository.Tools;

namespace Backups.InMemoryRepository;

public class Directory : RepositoryComponent
{
    private List<RepositoryComponent> _components = new List<RepositoryComponent>();
    public Directory(string name)
        : base(name)
    {
    }

    public RepositoryComponent RepParent { get; }
    public Directory DirParent { get; }
    public IReadOnlyCollection<RepositoryComponent> Components => _components;
    public override void Add(RepositoryComponent component)
    {
        if (component == null)
        {
            throw new BackupsException("Null reference of component");
        }

        component.Parent = this;
        _components.Add(component);
    }

    public override void Remove(RepositoryComponent component)
    {
        if (component == null)
        {
            throw new BackupsException("Null reference of component");
        }

        component.Parent = null;
        _components.Remove(component);
    }

    public override IReadOnlyCollection<RepositoryComponent> GetComponents()
    {
        return Components;
    }
}