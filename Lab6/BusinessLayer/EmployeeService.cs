using DataAccessLayer;
using DataAccessLayer.Algorithm;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;

namespace BusinessLayer;

public class EmployeeService : IEmployeeService
{
    private Employee _employee;

    public EmployeeService(Employee employee)
    {
        _employee = employee ?? throw new MessageException("null reference of worker");
    }

    public IReadOnlyCollection<Messenger> GetUnreadMessages()
    {
        var processing = new List<Messenger>();
        foreach (var messenger in MessengerData.GetInstance().Messengers)
        {
            foreach (var message in messenger.Messages)
            {
                if (message.GetMessageStatus() == MessageStatus.Processed)
                {
                    processing.Add(messenger);
                }
            }
        }
        return processing;
    }

    public void Actions(IRequest action, Messenger messenger)
    {
        if (action == null || messenger == null)
            throw new MessageException("null reference");
        action.Request(messenger);
    }

    public void Actions(IRequest action, string? message, Messenger messenger)
    {
        if (action == null || messenger == null || string.IsNullOrWhiteSpace(message))
            throw new MessageException("null reference");
        action.Request(messenger, message);
    }
}