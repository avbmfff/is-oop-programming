using System.Text.RegularExpressions;
using Isu.Entities;
using Isu.Exception;

using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class OgnpGroup
{
    private const int LenghtOfGroupName = 5;
    private const int StartLetterIndex = 0;
    private const int LenghtOfLetterIndex = 1;
    private const int MaxStudentCount = 20;
    private const string FacultyPattern = "^[a-zA-Z]+$";
    private static Regex _letter = new Regex(FacultyPattern, RegexOptions.Compiled | RegexOptions.Singleline);
    private char _facultyLetter;
    private List<Student> _students;

    public OgnpGroup(char facultyLetter, string nameOfCourse, GroupName groupName, Lesson lesson)
    {
        if (string.IsNullOrWhiteSpace(nameOfCourse) || groupName == null || lesson == null)
        {
            throw new IsuException("Null reference argument");
        }

        if (!_letter.IsMatch(facultyLetter.ToString()))
        {
            throw new IsuException("Invalid faculty letter");
        }

        _facultyLetter = facultyLetter;
        NameOfCourse = nameOfCourse;
        GroupName = groupName;
        Lesson = lesson;
        _students = new List<Student>();
    }

    public IReadOnlyCollection<Student> Students => _students;
    public string NameOfCourse { get; private set; }
    public Lesson Lesson { get; private set; }
    public GroupName GroupName { get; private set; }

    public void SetGroupName(GroupName value)
    {
        if (value == null)
        {
            throw new IsuException("Invalid group name");
        }

        GroupName = value;
    }

    public void SetCourseName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new IsuException("Invalid group name");
        }

        NameOfCourse = value;
    }

    public void SetLesson(Lesson value)
    {
        if (value == null)
        {
            throw new IsuException("Invalid data");
        }

        Lesson = value;
    }

    public void AddStudent(Student student)
    {
        string letterOfGroup = student.Group.GroupName.Name.Substring(StartLetterIndex, LenghtOfLetterIndex);
        if (student == null && Students.Contains(student) && Students.Count > MaxStudentCount && letterOfGroup == _facultyLetter.ToString())
        {
            throw new IsuException("Can't add student");
        }

        _students.Add(student!);
    }

    public void DeleteStudent(Student student)
    {
        if (student == null && !Students.Contains(student))
        {
            throw new IsuException("Student doesn't exist");
        }

        _students.Remove(student!);
    }

    public void ChangeGroupName(GroupName groupname)
    {
        if (groupname.Name.Length != LenghtOfGroupName)
        {
            throw new IsuException("Invalid group name");
        }

        GroupName = groupname;
    }

    public override bool Equals(object obj)
    {
        return obj is OgnpGroup ognpGroup && ognpGroup.GroupName == GroupName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_facultyLetter, _students, NameOfCourse, Lesson, GroupName);
    }
}
