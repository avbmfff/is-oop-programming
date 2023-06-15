using System.Net.Mail;
using DataAccessLayer.Messages;

namespace DataAccessLayer.Algorithm;

public class Review : IRequest
{
    public void Request(Messenger messenger)
    {
        throw new MessageException("indefinite");
    }

    public void Request(Messenger messenger, string? answer)
    {
        if (messenger == null || string.IsNullOrWhiteSpace(answer))
            throw new MessageException("null reference of exception");
        messenger.GetLastMessage().ChangeStatus(MessageStatus.Accepted);
        messenger.GetLastMessage().SetAnswer(answer);
    }
}