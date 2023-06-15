using DataAccessLayer.Algorithm;
using DataAccessLayer.Messages;

namespace BusinessLayer;

public interface IEmployeeService
{
    IReadOnlyCollection<Messenger> GetUnreadMessages();
    void Actions(IRequest action, Messenger messenger);
    void Actions(IRequest action, string? message, Messenger messenger);
}