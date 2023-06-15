using System.IO.Pipes;
using Isu.Entities;
using Isu.Exception;
using Isu.Extra.Models;
using Isu.Models;
using Isu.Services;

namespace Isu.Extra.Entities;

public class Schedule
{
    private GroupName _group;
    private List<Lesson> _lessons;
    public Schedule(GroupName group)
    {
        _group = group ?? throw new IsuException("Invalid name");
        _lessons = new List<Lesson>();
    }

    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public GroupName GetGroup(GroupName value)
    {
        return _group;
    }

    public void SetGroup(GroupName value)
    {
        _group = value ?? throw new IsuException("Invalid name");
        _group = value;
    }

    public void AddLesson(Lesson lesson)
    {
        if (ExistLessonForName(lesson))
        {
            throw new IsuException("Lesson already exist");
        }

        _lessons.Add(lesson ?? throw new ArgumentNullException(nameof(lesson)));
    }

    public void DeleteLesson(Lesson lesson)
    {
        if (!ExistLessonForName(lesson))
        {
            throw new IsuException("Lesson doesn't exist");
        }

        _lessons.Remove(lesson ?? throw new ArgumentNullException(nameof(lesson)));
    }

    private bool ExistLessonForTime(Lesson lesson)
    {
        return _lessons.Any(lessons => lessons.Time == lesson.Time && lessons.DayOfTheWeek == lesson.DayOfTheWeek);
    }

    private bool ExistLessonForName(Lesson lesson)
    {
        return _lessons.All(lessons => lessons.Time != lesson.Time || lessons.DayOfTheWeek != lesson.DayOfTheWeek || lessons.Name != lesson.Name);
    }
}