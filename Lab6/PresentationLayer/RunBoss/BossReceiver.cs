using System.Net.Mail;
using BusinessLayer;
using BusinessLayer.Authorization;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace PresentationLayer.RunBoss;

public class BossReceiver
{
    private IBossService _iBossService;

    public BossReceiver(Boss boss)
    {
        if (boss == null)
            throw new MessageException("Null reference of boss");
        _iBossService = new BossService(boss);
    }

    public void RegisterWorker()
    {
        Console.WriteLine("Please enter the email and then enter the name");
        _iBossService.CreateWorker(new MailAddress(Console.ReadLine()), Console.ReadLine());
        Console.WriteLine("Success!");
    }

    public void AddWorker()
    {
        Console.WriteLine("Please enter the email");
        ICheck _icheck = null;
        _iBossService.AddWorker(_icheck.GetWorker(new MailAddress(Console.ReadLine())));
        Console.WriteLine("Success!");
    }
    
    public void GetReport()
    {
        Console.WriteLine("Enter the path to report");
        _iBossService.GetReport(Console.ReadLine());
        Console.WriteLine("Success!");
    }
}