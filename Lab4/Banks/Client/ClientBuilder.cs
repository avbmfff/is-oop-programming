using System.Net.Mail;

namespace Banks;

public class ClientBuilder
{
    private Client.Client _client;

    public ClientBuilder()
    {
        _client = new Client.Client();
    }

    public ClientBuilder SetFullName(string name, string lastname)
    {
        _client.SetFullName(name, lastname);
        return this;
    }

    public ClientBuilder SetPassport(char[] series, char[] number, DateTime registerDate, string registerlocation)
    {
        var passport = new Passport(series, number, registerDate, registerlocation);
        _client.SetPassport(passport);
        return this;
    }

    public ClientBuilder SetBirthday(DateOnly birthday)
    {
        _client.SetBirthday(birthday);
        return this;
    }

    public ClientBuilder SetPhone(char[] phonenumber)
    {
        _client.SetPhone(phonenumber);
        return this;
    }

    public ClientBuilder SetEmail(MailAddress mailAddress)
    {
        _client.SetEMail(mailAddress);
        return this;
    }

    public Client.Client Build()
    {
        return _client;
    }
}