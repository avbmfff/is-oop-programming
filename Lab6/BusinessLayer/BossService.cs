using System.Net;
using System.Net.Mail;
using BusinessLayer.Authorization;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLayer;

public class BossService : IBossService
{
    private Boss _boss;

    public BossService(Boss boss)
    {
        _boss = boss ?? throw new MessageException("null reference of boss");
    }

    public Employee CreateWorker(MailAddress mailAddress, string name)
    {
        if (mailAddress == null || string.IsNullOrWhiteSpace(name))
            throw new MessageException("null reference");
        var worker = new Employee(name, mailAddress);
        string? password = new Random().ToString();
        var message = new MailMessage(_boss.MailAddress, mailAddress);
        message.Subject = "Your login is" + mailAddress + " " + "Your password is" + password;
        var smtp = new SmtpClient("smtp.gmail.com", 587);
        smtp.Credentials = new NetworkCredential(_boss.MailAddress.Address, _boss.GetPassword());
        smtp.EnableSsl = true;
        smtp.Send(message);
        var newAccount = new Account(new Random().ToString()!, worker);
        AccountData.GetInstance().AddAccount(newAccount);
        return worker;
    }

    public void AddWorker(Employee employee)
    {
        if (employee == null)
            throw new MessageException("null reference of worker");
        this._boss.AddWorker(employee);
    }

    public void GetReport(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new MessageException("null reference of path");
        _boss.GetReport(path);
    }
}