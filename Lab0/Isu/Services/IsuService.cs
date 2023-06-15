using System.Net.Http.Headers;
using Isu.Entities;
using Isu.Exception;
using Isu.Models;

namespace Isu.Services;

[Serializable]
public static class GroupsData // принадлежит собственному типу, а не объекту
{
    public static List<Group> Groups { get; set; } = new List<Group>();
}

public class IsuService : IIsuService
{
    public Group AddGroup(GroupName name)
    {
        var newGroup = new Group(name);

        foreach (var group in GroupsData.Groups)
        {
            if (group.GroupName == name)
            {
                throw new IsuException("Can't add");
            }
        }

        GroupsData.Groups.Add(newGroup);

        return newGroup;
    }

    public Student AddStudent(Group group, string name)
    {
        if (group != null)
        {
            return group.AddStudent(new Student(name, group));
        }

        throw new IsuException("Can't add");
    }

    public Student GetStudent(int id)
    {
        foreach (var student in from @group in GroupsData.Groups from student in @group.Students where student.Id == id select student)
        {
            return student;
        }

        throw new IsuException("Can't get");
    }

    public Student? FindStudent(int id)
    {
        foreach (var student in from @group in GroupsData.Groups from student in @group.Students where student.Id == id select student)
        {
            return student;
        }

        return null;
    }

    public IReadOnlyCollection<Student> FindStudents(GroupName groupName)
    {
        foreach (var group in GroupsData.Groups.Where(group => group.GroupName == groupName))
        {
            return group.Students;
        }

        return new List<Student>();
    }

    public IReadOnlyCollection<Student> FindStudents(CourseNumber courseNumber)
    {
        foreach (var group in GroupsData.Groups.Where(group => group.CourseNumber == courseNumber))
        {
            return group.Students;
        }

        return new List<Student>();
    }

    public Group? FindGroup(GroupName groupName)
    {
        return GroupsData.Groups.Where(group => group.GroupName == groupName).FirstOrDefault();
    }

    public IReadOnlyCollection<Group> FindGroups(CourseNumber courseNumber)
    {
        var sameCourseNumber = new List<Group>();
        foreach (Group group in GroupsData.Groups)
        {
            if (group.CourseNumber == courseNumber)
            {
                sameCourseNumber.Add(group);
            }

            return sameCourseNumber;
        }

        return new List<Group>();
    }

    public void ChangeStudentGroup(Student student, Group newGroup)
    {
        if (!newGroup.GroupIsFull())
        {
            student.Group.DeleteStudent(student);
            student.Group = newGroup;
            newGroup.AddStudent(student);
        }
    }
}