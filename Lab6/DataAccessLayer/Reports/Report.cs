using System.Runtime.InteropServices.ComTypes;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;
using Word = Microsoft.Office.Interop.Word;

namespace DataAccessLayer.Reports;

public struct Report
{
    private Employee _employee;
    private List<Message> _message;
    private string _name;

    public Report(Employee employee, List<Message> message)
    {
        if (employee == null || message == null)
            throw new MessageException("null reference of data");
        _employee = employee;
        _message = message;
        _name = _employee.GetName() + " " + DateTime.Now;
    }

    public IReadOnlyCollection<string> GetReport()
    {
        string worker = "Accepted: " + _employee.GetName();
        string count = "For day: " + _message.Count;
        return new List<string>() {_name, worker};
    }
}