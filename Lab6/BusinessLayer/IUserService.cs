using System.Net.Mail;
using DataAccessLayer.Entities;
using DataAccessLayer.Messages;
using DataAccessLayer.Models;

namespace BusinessLayer;

public interface IUserService
{
    Messenger NewUser();
    void AddMessage(Messenger messenger, string message);
}