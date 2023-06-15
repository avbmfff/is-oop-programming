// See https://aka.ms/new-console-template for more information

using System.Net.Mail;
using BusinessLayer;
using BusinessLayer.Authorization;
using DataAccessLayer.Entities;
using PresentationLayer.RunBoss;
using PresentationLayer.RunUser;
using PresentationLayer.RunWorker;

ICheck icheck = null;
Console.WriteLine("Choose your role, please");
Console.WriteLine("Press the 0 for user role, 1 for worker and 2 for boss");
var answer = Console.ReadLine();

bool checking;
Invoker invoker;
Employee employee = null;
Boss boss = null;
switch (answer)
{
    case "0":
        invoker = new Invoker();
        invoker.SetCommand(new UserCommand());
        invoker.StartWork();
        break;
    case "1":
        Console.WriteLine("Please, write your email address and password");
        checking = false;
        while (checking == false)
        {
            MailAddress mailAddress = new MailAddress(Console.ReadLine());
            string password = Console.ReadLine();
            checking = icheck!.CheckAccount(mailAddress, password);
            if (checking == false)
                Console.WriteLine("Please, try again. Password or login is not true");
            else
            {
                employee = icheck.GetWorker(mailAddress);
            }
            invoker = new Invoker();
            var workerCommand = new EmployeeCommand();
            workerCommand.SetReceiver(new EmployeeReceiver(employee));
            invoker.SetCommand(new EmployeeCommand());
            invoker.StartWork();
        }
        
        break;
    case "2":
        Console.WriteLine("Please, write your email address and password");
        checking = false;
        while (checking == false)
        {
            MailAddress mailAddress = new MailAddress(Console.ReadLine());
            string password = Console.ReadLine();
            checking = icheck!.CheckAccount(mailAddress, password);
            if (checking == false)
                Console.WriteLine("Please, try again. Password or login is not true");
            else
            {
                boss = icheck.GetBoss(mailAddress);
            }
            invoker = new Invoker();
            var bossCommand = new BossCommand();
            bossCommand.SetReceiver(new BossReceiver(boss));
            invoker.SetCommand(new BossCommand());
            invoker.StartWork();
        }
        break;
}