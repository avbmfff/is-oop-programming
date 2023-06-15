using System.Net.Mail;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Models;

namespace BusinessLayer.Authorization;

public class AccountData : ICheck
{
    private static AccountData instance;
    private List<Account> _accounts;

    private AccountData()
    {
        _accounts = new List<Account>();
    }
    
    public void AddAccount(Account account)
    {
        if (account == null)
            throw new MessageException("null reference of account");
        _accounts.Add(account);
    }

    public IReadOnlyCollection<Account> Accounts => _accounts;

    public static AccountData GetInstance()
    {
        if (instance == null)
            instance = new AccountData();
        return instance;
    }

    public bool CheckAccount(MailAddress mailAddress, string password)
    {
        if (mailAddress == null || string.IsNullOrWhiteSpace(password))
            throw new MessageException("Null reference of data");
        return (from account in _accounts where account.MailAddress == mailAddress select account.TruePassword(password)).FirstOrDefault();
    }

    Employee ICheck.GetWorker(MailAddress mailAddress)
    {
        if (mailAddress == null)
            throw new MessageException("Null reference of data");
        return (from account in _accounts where account.MailAddress == mailAddress select account.Employee).FirstOrDefault();
    }

    Boss ICheck.GetBoss(MailAddress mailAddress)
    {
        if (mailAddress == null)
            throw new MessageException("Null reference of data");
        return (from account in _accounts where account.MailAddress == mailAddress select (Boss)account.Employee).FirstOrDefault();
    }
}