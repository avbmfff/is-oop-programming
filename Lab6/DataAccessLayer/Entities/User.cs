using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using DataAccessLayer.Messages;

namespace DataAccessLayer.Entities;

public class User
{
    private Message _message;
    public User(string userMessage, MailAddress userEmail)
    {
        if (string.IsNullOrWhiteSpace(userMessage) || userEmail == null)
        {
            throw new MessageException("the null reference of data");
        }
        _message = new Message(userMessage, this);
        UserEmail = userEmail;
    }

    public MailAddress UserEmail { get; }

    public Message GetMessage()
    {
        return _message;
    }

}