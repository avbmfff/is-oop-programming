using DataAccessLayer.Messages;

namespace DataAccessLayer.Algorithm;

public interface IRequest
{
    void Request(Messenger messenger);
    void Request(Messenger messenger, string? answer);
}