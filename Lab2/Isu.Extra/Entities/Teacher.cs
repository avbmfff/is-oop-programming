using Isu.Exception;
namespace Isu.Extra.Entities;

public class Teacher
{
    private const int LimitDegree = 0;
    private int _id;
    public Teacher(int id, string name, string lastName)
    {
        if (id <= LimitDegree || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName))
        {
            throw new IsuException("Null reference or invalid argument");
        }

        _id = id;
        Name = name;
        LastName = lastName;
    }

    public string Name { get; private set; }
    public string LastName { get; private set; }

    public void SetName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new IsuException("Null reference of argument");
        }

        Name = value;
    }

    public void SetLastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new IsuException("Null reference of argument");
        }

        LastName = value;
    }
}