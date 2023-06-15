using DataAccessLayer.Messages;

namespace DataAccessLayer.Algorithm;

public class Browse : IRequest
{
    public void Request(Messenger messenger)
    {
        if (messenger == null)
            throw new MessageException("null reference of exception");
        messenger.GetLastMessage().ChangeStatus(MessageStatus.Accepted);
    }

    public void Request(Messenger messenger, string? answer)
    {
        throw new MessageException("indefinite");
    }
}