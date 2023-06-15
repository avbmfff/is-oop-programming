using Isu.Entities;
using Isu.Exception;
using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Models;

public class OgnpGroupsData
{
    private List<OgnpGroup> _groups;

    public OgnpGroupsData()
    {
        _groups = new List<OgnpGroup>();
    }

    public IReadOnlyCollection<OgnpGroup> Groups => _groups;

    public void AddOgnpGroup(OgnpGroup group)
    {
        if (group == null || Contains(group))
        {
            throw new IsuException("Invalid argument of group");
        }

        _groups.Add(group);
    }

    public void RemoveOgnpGroup(OgnpGroup group)
    {
        if (group == null || Contains(group))
        {
            throw new IsuException("Invalid argument of group");
        }

        _groups.Remove(group);
    }

    public bool ContainsOgnpGroup(OgnpGroup group)
    {
        return Contains(group);
    }

    private bool ExistStudent(Student student)
    {
        return Groups.Any(group => group.Students.Contains(student));
    }

    private bool Contains(OgnpGroup group)
    {
        return _groups.Any(ognpgroup => ognpgroup.Equals(group));
    }
}