using System.Net.Mail;
using Banks.Observe;

namespace Banks.Client;

public class Client : IObserver
{
    private const int LimitDegreeOfPhoneNumber = 11;
    private char[] _phone = new char[LimitDegreeOfPhoneNumber];
    private string _name;
    private string _lastName;
    private DateOnly _birthday;
    private MailAddress _mailAddress;
    private Passport _passport;
    private List<Accounts.Account> _accounts = new List<Accounts.Account>();

    public IReadOnlyCollection<Accounts.Account> Accounts => _accounts;
    public static ClientBuilder CreateBuilder()
    {
        return new ClientBuilder();
    }

    public void AddNewAccount(Accounts.Account account)
    {
        if (account == null)
        {
            throw new BanksException("null reference of account");
        }

        if (ContainsAccount(account))
        {
            throw new BanksException("account doesn't exist");
        }

        _accounts.Add(account);
    }

    public void RemoveAccount(Accounts.Account account)
    {
        if (account == null)
        {
            throw new BanksException("null reference of account");
        }

        if (ContainsAccount(account))
        {
            throw new BanksException("account already exist");
        }

        _accounts.Remove(account);
    }

    public IReadOnlyCollection<Accounts.Account> GetAccount()
    {
        return Accounts;
    }

    public void SetFullName(string name, string lastname)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastname))
        {
            throw new BanksException("Incorrect name or lastname, please try again");
        }

        _name = name;
        _lastName = lastname;
    }

    public string GetFullName()
    {
        return _name + " " + _lastName;
    }

    public void SetBirthday(DateOnly birthday)
    {
        _birthday = birthday;
    }

    public DateOnly GetBirthday()
    {
        return _birthday;
    }

    public void SetEMail(MailAddress mailAddress)
    {
        if (mailAddress == null)
        {
            throw new BanksException("Null reference of Email");
        }

        _mailAddress = mailAddress;
    }

    public MailAddress GetEMail()
    {
        return _mailAddress;
    }

    public void SetPhone(char[] phone)
    {
        if (phone.Length != 11 || !CheckForDigit(phone))
        {
            throw new BanksException("Incorrect phone number, please try again");
        }

        _phone = phone;
    }

    public string GetPhone()
    {
        return _phone.Aggregate(" ", (current, value) => current + value.ToString());
    }

    public void SetPassport(Passport passport)
    {
        _passport = passport ?? throw new BanksException("Incorrect passport data, please tru again");
    }

    public string GetPassport()
    {
        return _passport.GetSeriesAndNumber();
    }

    public override bool Equals(object obj)
    {
        return obj is Client client && client.GetPassport() == GetPassport();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_phone, _name, _lastName, _birthday, _mailAddress, _passport);
    }

    public void Update(object ob)
    {
        var info = (ClientInfo)ob;
    }

    private bool CheckForDigit(IEnumerable<char> digits)
    {
        return digits.All(value => char.IsDigit(value));
    }

    private bool ContainsAccount(Accounts.Account account)
    {
        return _accounts.Any(value => value.Equals(account));
    }
}