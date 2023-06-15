using System.Net.Mail;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLayer.Authorization;

public interface ICheck
{
    bool CheckAccount(MailAddress mailAddress, string password);
    Employee GetWorker(MailAddress mailAddress);
    Boss GetBoss(MailAddress mailAddress);
}