using System.Net.Mail;
using System.Xml.Schema;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Messages;

public class Message
{
    private MessageStatus _status;
    private string _message;
    private string? _answer;

    public Message(string message, User user)
    {
        if (user == null || string.IsNullOrWhiteSpace(message))
        {
            throw new MessageException("message or user is null");
        }
        ChangeStatus(MessageStatus.Created);
        _message = message;
        User = user;
    }

    public User User { get; }

    public string GetMessage()
    {
        return User.UserEmail + " asked: " + _message + " " + "Answer: " + _answer;
    }
    
    public MessageStatus GetMessageStatus()
    {
        return _status;
    }
    public void SetAnswer(string? answer)
    {
        if (string.IsNullOrWhiteSpace(answer))
            throw new MessageException("null reference if answer");
        _answer = answer;
    }
    public void ChangeStatus(MessageStatus messageStatus)
    {
        if (messageStatus == null)
            throw new MessageException("null reference of status");
        _status = messageStatus;
    }
}