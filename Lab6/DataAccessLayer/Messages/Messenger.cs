using DataAccessLayer.Entities;

namespace DataAccessLayer.Messages;

public class Messenger
{
    private List<Message> _messages = new List<Message>();

    public Messenger(User user)
    {
        if (user == null)
            throw new MessageException("null reference of exception");
        User = user;
        _messages.Add(User.GetMessage());
        User.GetMessage().ChangeStatus(MessageStatus.Processed);
    }

    public User User { get; }
    public IReadOnlyCollection<Message> Messages => _messages;

    public void AddMessage(Message message)
    {
        if (message.User != _messages.Last().User)
            throw new MessageException("not your dialog");
        _messages.Add(message);
        message.ChangeStatus(MessageStatus.Processed);
    }
    
    public void SetAnswer(string? answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new MessageException("null reference");
        _messages.Last().SetAnswer(answer);
    }

    public Message GetLastMessage()
    {
        return _messages.Last();
    }
}