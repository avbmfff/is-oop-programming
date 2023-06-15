using System.Net.Mail;
using System.Security;
using BusinessLayer;
using DataAccessLayer.Algorithm;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;
using DataAccessLayer.Models;

namespace MessageTest;

public class MessageTests
{
    private static Boss _boss = new Boss("Andrey", new MailAddress("mama@mail.ru"), new SecureString());
    private IBossService _iboss = new BossService(_boss);
    private static Employee _employee = new Employee("Alalala", new MailAddress("nanandls@jdj.ru"));
    private static User _user = new User("help", new MailAddress("dfdhd@hoook.lw"));
    private IUserService _iuser = new UserService(_user);
    private IEmployeeService _iworker = new EmployeeService(_employee);
    [Fact]
    public void CreateWorker()
    {
        _iboss.AddWorker(_employee);
        Assert.True(_boss.WorkerExist(_employee));
    }
    
    [Fact]
    public void AddMessenger()
    {
        var newMessenger = _iuser.NewUser();
        Assert.True(MessengerData.GetInstance().MessengerExist(newMessenger));
    }
    
    [Fact]
    public void CheckMessageStatus()
    {
        Assert.True(_user.GetMessage().GetMessageStatus() == MessageStatus.Created);
        var newMessenger = _iuser.NewUser();
        Assert.True(_user.GetMessage().GetMessageStatus() == MessageStatus.Processed);
    }
    
    [Fact]
    public void RequestMessageBrowse()
    {
        var newMessenger = _iuser.NewUser();
        _iworker.Actions(new Browse(), newMessenger);
        Assert.True(_user.GetMessage().GetMessageStatus() == MessageStatus.Accepted);
    }
    
    [Fact]
    public void RequestMessageAnswer()
    {
        var newMessenger = _iuser.NewUser();
        _iworker.Actions(new Review(), "hi", newMessenger);
        Assert.True(_user.GetMessage().GetMessageStatus() == MessageStatus.Accepted);
        Assert.True(newMessenger.GetLastMessage().GetMessage() == "dfdhd@hoook.lw asked: help Answer: hi");
    }
}