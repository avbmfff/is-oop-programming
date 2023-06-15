using System.Net.Mail;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Models;

public class Account
{
    private string _password;
    
    public Account(string password, Employee employee)
    {
        if (string.IsNullOrWhiteSpace(password) || employee == null)
            throw new MessageException("null reference of data");
        _password = password;
        Employee = employee;
        MailAddress = employee.MailAddress;
    }
    
    public Employee Employee { get; }
    public MailAddress MailAddress { get; }

    public bool TruePassword(string password)
    {
        return !string.IsNullOrWhiteSpace(password) && password == _password;
    }
}