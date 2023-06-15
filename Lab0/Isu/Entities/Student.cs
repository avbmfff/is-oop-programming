using Isu.Exception;

namespace Isu.Entities;

public class Student
{
    public Student(string name = "", Group group = null!, string lastname = "", int id = 0)
    {
        Lastname = lastname;
        Name = name;
        Group = group;
        Id = id;
    }

    public string Lastname { get; private set; }
    public string Name { get; private set; }
    public Group Group { get; internal set; }

    public int Id { get; }

    public void SetLastName(string lastname)
    {
        if (lastname != null!) Lastname = lastname;
        else throw new IsuException("Invalid lastname");
    }

    public void SetName(string name)
    {
        if (name != null!) Name = name;
        else throw new IsuException("Invalid name");
    }
}