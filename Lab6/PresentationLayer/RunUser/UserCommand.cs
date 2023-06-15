using BusinessLayer;
using DataAccessLayer;

namespace PresentationLayer.RunUser;

public class UserCommand : ICommand
{
    private UserReceiver _userReceiver;

    public void SetReceiver(UserReceiver userReceiver)
    {
        if (userReceiver == null)
            throw new MessageException("Null reference");
        _userReceiver = userReceiver;
    }
    
    public void Execute()
    {
        Console.WriteLine("If you new user, please enter 0, send message - 1, exit - 2");
        var answer = Console.ReadLine();
        switch (answer)
        {
            case "0":
                _userReceiver.Register();
                break;
            case "1":
                _userReceiver.WriteMessage();
                break;
            case "2":
                Undo();
                break;
        }
    }

    public void Undo()
    {
        Console.WriteLine("Exit");
    }
}