using System.Net.Mail;
using BusinessLayer;
using BusinessLayer.Authorization;
using DataAccessLayer;
using DataAccessLayer.Algorithm;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;

namespace PresentationLayer.RunWorker;

public class EmployeeReceiver
{
    private IEmployeeService _iEmployeeService;

    public EmployeeReceiver(Employee employee)
    {
        if (employee == null)
            throw new MessageException("Null reference of boss");
        _iEmployeeService = new EmployeeService(employee);
    }

    public void Work()
    {
        IReadOnlyCollection <Messenger> _unread = _iEmployeeService.GetUnreadMessages();
        foreach (var value in _unread)
        {
            Console.WriteLine("Please, choose action. Browse - 0, Request - 1, exit - 2");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "0":
                    _iEmployeeService.Actions(new Browse(), value);
                    break;
                case "1":
                    Console.WriteLine("Please, write the answer");
                    _iEmployeeService.Actions(new Review(), Console.ReadLine(), value );
                    break;
            }
            if (answer == "2")
                break;
        }
    }
}