using DataAccessLayer;
using PresentationLayer.RunUser;

namespace PresentationLayer.RunWorker;

public class EmployeeCommand : ICommand
{
    private EmployeeReceiver _employeeReceiver;

    public void SetReceiver(EmployeeReceiver employeeReceiver)
    {
        if (employeeReceiver == null)
            throw new MessageException("Null reference");
        _employeeReceiver = employeeReceiver;
    }
    public void Execute()
    {
        Console.WriteLine("Start to work - 0, finish - 1");
        switch (Console.ReadLine())
        {
            case "0":
                _employeeReceiver.Work();
                break;
            case "1":
                Undo();
                break;
        }
    }

    public void Undo()
    {
        Console.WriteLine("Exit");
    }
}