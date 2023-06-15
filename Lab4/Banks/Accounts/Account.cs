namespace Banks.Accounts;

public abstract class Account
{
    private const int LimitMinDegree = 0;
    private const int DaysInYear = 365;
    private Guid _id;
    public Account(Bank bank, Client.Client client)
    {
        if (bank == null || client == null)
        {
            throw new BanksException("Invalid data");
        }

        _id = Guid.NewGuid();
        Sum = 0;
        CreatingTime = DateTime.Now;
        Bank = bank;
        Client = client;
    }

    protected float Sum { get; set; }
    protected Client.Client Client { get; }
    protected Bank Bank { get; }
    protected DateTime CreatingTime { get; }
    public Guid GetId()
    {
        return _id;
    }

    public float GetSum()
    {
        return Sum;
    }

    public virtual void AddSum(float value) { }

    public virtual void WithdrawSum(float value) { }

    public virtual float GetPercentSum(DateTime period)
    {
        return 0;
    }

    public virtual void AddPercentSum() { }
    public virtual void CountingPercentSum() { }

    public override bool Equals(object obj)
    {
        return obj is Account account && account._id == _id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, Sum, CreatingTime, Client, Bank);
    }

    public Client.Client GetClient()
    {
        return Client;
    }

    public Bank GetBank()
    {
        return Bank;
    }

    protected bool CheckClientsForDoubt(Client.Client client)
    {
        if (client == null)
        {
            throw new BanksException("Null reference of client");
        }

        return client.GetPassport() == null;
    }

    protected bool CancelTransaction(Client.Client client)
    {
        return CentralBank.GetInstanceCentralBank().BadClients.Any(value => value.GetPassport() == client.GetPassport());
    }
}