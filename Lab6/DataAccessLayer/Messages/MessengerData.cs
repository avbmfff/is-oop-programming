namespace DataAccessLayer.Messages;

public class MessengerData
{
    private static MessengerData instance;
    private List<Messenger> _messengers;
    public IReadOnlyCollection<Messenger> Messengers => _messengers;

    private MessengerData()
    {
        _messengers = new List<Messenger>();
    }
    public void AddMessenger(Messenger messenger)
    {
        if (messenger == null)
            throw new MessageException("null reference of message");
        _messengers.Add(messenger);
    }
    
    public void AddMessenger(List<Messenger> messenger)
    {
        if (messenger == null)
            throw new MessageException("null reference of messages");
        foreach (var message in messenger.Where(MessengerExist))
        {
            _messengers.Add(message);
        }
    }

    public static MessengerData GetInstance()
    {
        if (instance == null)
            instance = new MessengerData();
        return instance;
    }
    public bool MessengerExist(Messenger messenger)
    {
        return _messengers.Any(val => val.User == messenger.User);
    }
}