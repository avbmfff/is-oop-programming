using System.Collections.Concurrent;
using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Service;

public class IsuExtraService : IsuService
{
    private OgnpGroupsData _ognpGroupsData = new OgnpGroupsData();
    public OgnpGroup AddNewOgnpGroup(OgnpGroup ognpGroup)
    {
        if (!_ognpGroupsData.ContainsOgnpGroup(ognpGroup))
        {
            _ognpGroupsData.AddOgnpGroup(ognpGroup);
        }

        return ognpGroup;
    }

    public Lesson AddLessonInSchedule(GroupName groupName, Lesson lesson)
    {
        var schedule = new Schedule(groupName);
        schedule.AddLesson(lesson);
        return lesson;
    }

    public void JoinGroupOgnp(Student student, OgnpGroup ognpGroup)
    {
        AddLessonInSchedule(student.Group.GroupName, ognpGroup.Lesson);
        ognpGroup.AddStudent(student);
    }

    public void RemoveFromOgnpGroup(Student student, OgnpGroup ognpGroup)
    {
        ognpGroup.DeleteStudent(student);
    }

    public IReadOnlyCollection<OgnpGroup> GetClassOgnpGroups(string nameOfCourse)
    {
        return _ognpGroupsData.Groups.Where(group => group.NameOfCourse == nameOfCourse).ToList();
    }

    public IReadOnlyCollection<Student> GetStudentsListOgnpGroup(OgnpGroup group)
    {
        return group.Students;
    }

    public IReadOnlyCollection<string> GetStudentsWithoutOgnp(Group group)
    {
        return group.Students.Select(student => new
            {
                student, haveognp = _ognpGroupsData.Groups.Any(groups => groups.Students.Contains(student)),
            })
            .Where(@t => !t.haveognp)
            .Select(@t => t.student.Name).ToList();
    }

    public IReadOnlyCollection<OgnpGroup> GetListOgnpGroup()
    {
        return _ognpGroupsData.Groups;
    }
}