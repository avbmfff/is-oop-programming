using Banks.Accounts;
using Banks.Observe;

namespace Banks;

public class Bank : IObservable, IObserver
{
    private const int LimitDegree = 0;
    private readonly List<Client.Client> _clients;
    private List<IObserver> _observers;

    public Bank(string name, ClientInfo clientInfo)
    {
        if (string.IsNullOrWhiteSpace(name) || clientInfo == null)
        {
            throw new BanksException("Invalid degree or null reference of name");
        }

        Name = name;
        ClientInfo = clientInfo;
        _clients = new List<Client.Client>();
        _observers = new List<IObserver>();
    }

    public ClientInfo ClientInfo { get; private set; }
    public string Name { get; }
    public IReadOnlyCollection<Client.Client> Clients => _clients;
    public IReadOnlyCollection<IObserver> Observers => _observers;

    public void AddClient(Client.Client client)
    {
        if (client == null)
        {
            throw new BanksException("Null reference of client");
        }

        if (Contains(client))
        {
            throw new BanksException("Client already exist");
        }

        _clients.Add(client);
    }

    public void AddClients(List<Client.Client> clients)
    {
        if (clients == null)
        {
            throw new BanksException("Null reference of client");
        }

        foreach (Client.Client client in clients.Where(Contains))
        {
            _clients.Add(client);
        }
    }

    public void RemoveClient(Client.Client client)
    {
        if (client == null)
        {
            throw new BanksException("Null reference of client");
        }

        if (!Contains(client))
        {
            throw new BanksException("Client doesn't exist");
        }

        _clients.Remove(client);
    }

    public void RegisterObserver(IObserver observer)
    {
        if (observer == null)
        {
            throw new BanksException("Client doesn't exist");
        }

        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (observer == null)
        {
            throw new BanksException("Client doesn't exist");
        }

        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (IObserver o in _observers)
        {
            o.Update(ClientInfo);
        }
    }

    public void ChangeClientInfo(ClientInfo newvalue)
    {
        ClientInfo = newvalue ?? throw new BanksException("Null reference of value");
    }

    public Account CreateCreditAccount(Client.Client client, int value)
    {
        if (client == null || value < LimitDegree || !Contains(client))
        {
            throw new BanksException("Null reference of client");
        }

        var creditAccount = new CreditAccount(this, client, value);
        client.AddNewAccount(creditAccount);
        return creditAccount;
    }

    public Account CreateDepositAccount(Client.Client client, int value, int month)
    {
        if (client == null || value < LimitDegree || !Contains(client))
        {
            throw new BanksException("Null reference of client");
        }

        DateTime.Now.AddMonths(month);
        var depositAccount = new DepositAccount(this, client, DateTime.Now.AddMonths(month), value);
        client.AddNewAccount(depositAccount);
        return depositAccount;
    }

    public Account CreateDebitAccount(Client.Client client, float sum)
    {
        if (client == null || sum < LimitDegree || !Contains(client))
        {
            throw new BanksException("Null reference of client");
        }

        var debitAccount = new DebitAccount(this, client, sum);
        client.AddNewAccount(debitAccount);
        return debitAccount;
    }

    public void LocalTransfer(Client.Client toClient, Client.Client fromClient, Account toAccount, Account fromAccount, int sum)
    {
        if (toClient == null || fromClient == null || toAccount == null || fromAccount == null || sum < LimitDegree)
        {
            throw new BanksException("Null reference of data");
        }

        if (!Contains(fromClient) || fromAccount.GetClient() != fromClient || !Contains(toClient) || toAccount.GetClient() != toClient)
        {
            throw new BanksException("Data doesn't exist");
        }

        fromAccount.WithdrawSum(sum);
        toAccount.AddSum(sum);
    }

    public void TransferBetweenBanks(Bank bank, Client.Client fromClient, Account fromAccount, Client.Client toClient, Account toAccount, int sum)
    {
        if (toClient == null || fromClient == null || toAccount == null || fromAccount == null || sum < LimitDegree || bank == null)
        {
            throw new BanksException("Null reference of data");
        }

        if (!Contains(fromClient) || fromAccount.GetClient() != fromClient || !CentralBank.GetInstanceCentralBank().Contains(bank) || !bank.Contains(toClient))
        {
            throw new BanksException("Data doesn't exist");
        }

        toAccount.WithdrawSum(sum);
        fromAccount.AddSum(sum);
    }

    public IReadOnlyCollection<Client.Client> GetListClient()
    {
        return Clients;
    }

    public override bool Equals(object obj)
    {
        return obj is Bank bank && bank.Name == Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_clients, Name);
    }

    public void Update(object ob)
    {
        var bInfo = (BankInfo)ob;
        foreach (var account in from value in _clients from account in value.Accounts where account.GetBank() == this select account)
        {
            account.AddPercentSum();
        }
    }

    private bool Contains(Client.Client client)
    {
        return Clients.Any(value => value.GetPassport() == client.GetPassport());
    }

    private bool CheckClientsForDoubt(Client.Client client)
    {
        if (client == null)
        {
            throw new BanksException("Null reference of client");
        }

        return client.GetPassport() == null;
    }
}