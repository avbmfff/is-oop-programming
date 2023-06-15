using Banks.Observe;

namespace Banks;

public class CentralBank : IObservable
{
    private static CentralBank _centralBank;
    private List<Bank> _banks;
    private List<Client.Client> _badClients;
    private BankInfo _bankInfo;
    private List<IObserver> _observers;

    private CentralBank()
    {
        Name = "Central Bank";
        _banks = new List<Bank>();
        _badClients = new List<Client.Client>();
        _observers = new List<IObserver>();
        _bankInfo = null;
    }

    public string Name { get; private set; }
    public IReadOnlyCollection<Bank> Banks => _banks;
    public IReadOnlyCollection<Client.Client> BadClients => _badClients;
    public IReadOnlyCollection<IObserver> Observers => _observers;

    public static CentralBank GetInstanceCentralBank()
    {
        return _centralBank ??= new CentralBank();
    }

    public void SetDate(DateTime value)
    {
        _bankInfo.SetDate(value);
    }

    public void AddBank(Bank bank)
    {
        if (bank == null)
        {
            throw new BanksException("Null reference of bank");
        }

        if (Contains(bank))
        {
            throw new BanksException("Bank already exist");
        }

        _banks.Add(bank);
    }

    public void RemoveBank(Bank bank)
    {
        if (bank == null)
        {
            throw new BanksException("Null reference of bank");
        }

        if (!Contains(bank))
        {
            throw new BanksException("Bank doesn't exist");
        }

        _banks.Remove(bank);
    }

    public void AddClient(Client.Client client)
    {
        if (client == null)
        {
            throw new BanksException("Null reference of client");
        }

        if (ContainsBadClients(client))
        {
            throw new BanksException("Client already exist");
        }

        _badClients.Add(client);
    }

    public void AddClients(List<Client.Client> clients)
    {
        if (clients == null)
        {
            throw new BanksException("Null reference of client");
        }

        foreach (Client.Client client in clients.Where(ContainsBadClients))
        {
            _badClients.Add(client);
        }
    }

    public void RemoveClient(Client.Client client)
    {
        if (client == null)
        {
            throw new BanksException("Null reference of client");
        }

        if (!ContainsBadClients(client))
        {
            throw new BanksException("Client doesn't exist");
        }

        _badClients.Remove(client);
    }

    public IReadOnlyCollection<Client.Client> GetListBadClient()
    {
        return BadClients;
    }

    public bool ContainsBadClients(Client.Client client)
    {
        return Enumerable.Contains(_badClients, client);
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
            o.Update(_bankInfo);
        }
    }

    public bool Contains(Bank bank)
    {
        return Enumerable.Contains(_banks, bank);
    }
}