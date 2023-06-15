using System.Net.Mail;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;

namespace BusinessLayer;

public class UserService : IUserService
{
    private User _user;

    public UserService(User user)
    {
        _user = user ?? throw new MessageException("null reference of user");
    }

    public Messenger NewUser()
    {
        var newMessenger = new Messenger(_user);
        MessengerData.GetInstance().AddMessenger(newMessenger);
        return newMessenger;
    }

    public void AddMessage(Messenger messenger, string message)
    {
        if (messenger == null)
            throw new MessageException("Null reference of messages");
        if (MessengerData.GetInstance().MessengerExist(messenger))
            throw new MessageException("messenger doesnt exist");
        messenger.AddMessage(new Message(message, _user));
    }
}