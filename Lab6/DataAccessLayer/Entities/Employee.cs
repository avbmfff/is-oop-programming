using System.Net.Mail;
using DataAccessLayer.Messages;

namespace DataAccessLayer.Entities;

public class Employee
{
    private const int LimitDegree = 0;
    private string _name;

    public Employee(string name, MailAddress mailAddress)
    {
        if (string.IsNullOrWhiteSpace(name) || mailAddress == null)
            throw new MessageException("null reference of data");
        _name = name;
        MailAddress = mailAddress;
        AmountMessages = new Dictionary<DateTime, int>();
    }
    public MailAddress MailAddress { get; }
    public Dictionary<DateTime, int> AmountMessages { get; }
    public string GetName()
    {
        return _name;
    }

    public void AddAmount(DateTime dateTime, int amount)
    {
        if (dateTime == DateTime.MinValue || amount < LimitDegree)
            throw new MessageException("Invalid data");
        AmountMessages.Add(dateTime, amount);
    }
}