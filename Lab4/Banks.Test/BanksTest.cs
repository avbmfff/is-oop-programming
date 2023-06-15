using System.Net.Mail;
using System.Reflection.Metadata;
using Banks.Accounts;
using Banks.Observe;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    [Fact]
    public void CheckAccount()
    {
        var forbank = new Dictionary<int, float>()
    {
        { 500, 5 },
        { 1000, 6 },
    };
        ClientInfo clientInfo1 = new ClientInfo(150, 6, forbank, 150);
        Bank bank1 = new Bank("Bel", clientInfo1);
        Client.Client client = new Client.Client();
        Passport passport = new Passport("1234".ToCharArray(), "565456".ToCharArray(), DateTime.Today, "65464");
        client.SetFullName("Anastasia", "Voronova");
        client.SetPassport(passport);
        bank1.AddClient(client);
        Account account = bank1.CreateDebitAccount(client, 20000);
        Assert.Contains(account, client.Accounts);
    }

    [Fact]
    public void TransferToOtherAccount()
    {
        var forbank = new Dictionary<int, float>()
        {
            { 500, 5 },
            { 1000, 6 },
        };
        ClientInfo clientInfo1 = new ClientInfo(150, 6, forbank, 150);
        Bank bank1 = new Bank("Bel", clientInfo1);
        Client.Client client = new Client.Client();
        Client.Client client1 = new Client.Client();
        Passport passport = new Passport("1234".ToCharArray(), "565456".ToCharArray(), DateTime.Today, "65464");
        Passport passport1 = new Passport("1734".ToCharArray(), "599996".ToCharArray(), DateTime.Today, "65464");
        client.SetFullName("Anastasia", "Voronova");
        client.SetPassport(passport);
        client1.SetFullName("LL", "ddd");
        client1.SetPassport(passport1);
        bank1.AddClient(client);
        bank1.AddClient(client1);
        Account account = bank1.CreateDebitAccount(client, 20000);
        Account account1 = bank1.CreateDebitAccount(client1, 30000);
        bank1.LocalTransfer(client, client1, account, account1, 5000);
        Assert.True(account1.GetSum() == 25000);
    }
}