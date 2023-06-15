using System.Runtime.CompilerServices;
using Isu.Exception;

namespace Isu.Models;

public class GroupName
{
    private const int Lenght = 5;
    private string _name;
    public GroupName(string name)
    {
        if (IsCorrectName(name))
        {
            _name = name;
            Name = name;
        }

        _name = null!;
        Name = _name;
    }

    public string Name { get; set; }
    public bool IsCorrectName(string name)
    {
        if (name.Length != Lenght)
        {
            throw new IsuException("Invalid name");
        }

        return true;
    }
}