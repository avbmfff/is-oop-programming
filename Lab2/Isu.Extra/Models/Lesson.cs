using System.Text.RegularExpressions;
using Isu.Exception;
using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Models;

public class Lesson
{
    public Lesson(string name, Enum dayOfTheWeek, TimeOnly time, GroupName group, Teacher teacher)
    {
        if (name != null || dayOfTheWeek != null || teacher != null)
        {
            Name = name;
            DayOfTheWeek = dayOfTheWeek;
            Time = time;
            Teacher = teacher;
        }
        else
        {
            throw new IsuException("Invalid data");
        }
    }

    public Lesson(string name, Enum dayOfTheWeek, TimeOnly time, GroupName group)
    {
        if (string.IsNullOrWhiteSpace(name) || dayOfTheWeek == null)
        {
            throw new IsuException("Null reference of argument");
        }

        Name = name;
        DayOfTheWeek = dayOfTheWeek;
        Time = time;
    }

    public string Name { get; private set; }
    public Enum DayOfTheWeek { get; private set; }
    public TimeOnly Time { get; private set; }
    public Teacher Teacher { get; private set; }

    public void SetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new IsuException("Null reference of argument");
        }

        Name = value;
    }

    public void SetDayOfTheWeek(Enum value)
    {
        DayOfTheWeek = value ?? throw new IsuException("Null reference of argument");
    }

    public void SetTime(TimeOnly value)
    {
        Time = value;
    }

    public void SetTeacher(Teacher value)
    {
        Teacher = value ?? throw new IsuException("Null reference of argument");
    }
}