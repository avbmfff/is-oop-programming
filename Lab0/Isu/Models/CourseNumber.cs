namespace Isu.Models;

public class CourseNumber
{
    private int _number;
    public CourseNumber(int number)
    {
        if (number > 0)
        {
            _number = number;
        }
    }
}