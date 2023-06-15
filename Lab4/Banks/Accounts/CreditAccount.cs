namespace Banks.Accounts;

public class CreditAccount : Account
{
    private const int LimitMinDegree = 0;
    private const int DaysInYear = 365;
    public CreditAccount(Bank bank, Client.Client client, float sum)
        : base(bank, client)
    {
        if (sum < LimitMinDegree)
        {
            throw new BanksException("sum doesn't exist");
        }

        Sum = sum;
    }

    public override void AddSum(float value)
    {
        if (CancelTransaction(Client))
        {
            throw new BanksException("Operation not possible");
        }

        if (Sum < LimitMinDegree)
        {
            Sum += value - Bank.ClientInfo.GetCommision();
        }
        else
        {
            Sum += value;
        }
    }

    public override void WithdrawSum(float value)
    {
        if (CancelTransaction(Client))
        {
            throw new BanksException("Operation not possible");
        }

        if (value > Bank.ClientInfo.GetTransaction() && CheckClientsForDoubt(Client))
        {
            throw new BanksException("can't do this transaction");
        }

        if (Sum < LimitMinDegree)
        {
            Sum -= value - Bank.ClientInfo.GetCommision();
        }
        else
        {
            Sum -= value;
        }
    }
}