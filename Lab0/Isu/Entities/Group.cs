using System.Linq;
using Isu.Exception;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    public const int MaxStudentsCount = 25;
    private const int Lenght = 5;
    private GroupName _groupName;
    private CourseNumber _courseNumber = null!;
    private List<Student> _students = new List<Student>();

    public Group(GroupName groupName)
    {
       if (groupName != null!) _groupName = groupName;
       else throw new IsuException("Invalid group name");
    }

    public Group(GroupName groupName, CourseNumber courseNumber)
    {
        _groupName = groupName;
        _courseNumber = courseNumber;
    }

    public IReadOnlyCollection<Student> Students => _students;
    public GroupName GroupName => _groupName;
    public CourseNumber CourseNumber => _courseNumber;

    public int GetCourseNumber(GroupName groupName)
    {
        string? group = groupName.ToString();
        if (group != null) return int.Parse(group.Substring(2, 1));
        return 0;
    }

    public void ChangeGroupName(string name)
    {
       if (IsCorrectName(name)) _groupName.Name = name;
    }

    public Student AddStudent(Student student)
    {
        if (_students.Count == MaxStudentsCount || student == null)
        {
            throw new IsuException("Can't Add student");
        }

        _students.Add(student);

        return student;
    }

    public bool GroupIsFull()
    {
        return _students.Count == MaxStudentsCount;
    }

    public void DeleteStudent(Student student)
    {
       if (student != null!) _students.Remove(student);
    }

    public Student FindStudent(string name)
    {
        foreach (Student student in _students.Where(student => student.Name == name))
        {
            return student;
        }

        return null!;
    }

    public Student FindStudent(int id) // не могу воспользоваться SingleOrDefault, подсвечивает красным и не распознаёт
    {
        foreach (Student student in _students.Where(student => student.Id == id))
        {
            return student;
        }

        return null!;
    }

    private bool IsCorrectName(string name)
    {
        if (name.Length == Lenght)
        {
            return true;
        }

        return false;
    }
}