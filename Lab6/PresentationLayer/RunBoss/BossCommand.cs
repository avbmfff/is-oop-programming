using DataAccessLayer;
using PresentationLayer.RunUser;

namespace PresentationLayer.RunBoss;

public class BossCommand : ICommand
{
    private BossReceiver _bossReceiver;

    public void SetReceiver(BossReceiver bossReceiverUser)
    {
        if (bossReceiverUser == null)
            throw new MessageException("Null reference");
        _bossReceiver = bossReceiverUser;
    }
    
    public void Execute()
    {
        Console.WriteLine("If you want to add new worker in data enter 0, add worker in your department enter 1, get the report - 2, exit - 3");
        var answer = Console.ReadLine();
        switch (answer)
        {
            case "0":
                _bossReceiver.RegisterWorker();
                break;
            case "1":
                _bossReceiver.AddWorker();
                break;
            case "2":
                _bossReceiver.GetReport();
                break;
            case "3":
                Undo();
                break;
        }
    }

    public void Undo()
    {
        Console.WriteLine("Exit");
    }
}