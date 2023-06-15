using BusinessLayer;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;

namespace PresentationLayer.RunUser;

public class UserReceiver
{
    private IUserService _iuser;
    private Messenger _messenger;

    public UserReceiver(User user)
    {
        if (user == null)
            throw new MessageException("Null reference of user");
        _iuser = new UserService(user);
    }

    public void Register()
    {
       _messenger = _iuser.NewUser();
        Console.WriteLine("Success!");
    }

    public void WriteMessage()
    {
        Console.WriteLine("Enter your message");
        _iuser.AddMessage(_messenger, Console.ReadLine());
        Console.WriteLine("Sent. Wait for an answer.");
    }
}