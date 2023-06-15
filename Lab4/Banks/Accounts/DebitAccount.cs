namespace Banks.Accounts;

public class DebitAccount : Account
{
    private const int LimitMinDegree = 0;
    private const int DaysInYear = 365;
    private float _percentSum = 0;
    private int _monthCounting = 0;

    public DebitAccount(Bank bank, Client.Client client, float sum)
        : base(bank, client)
    {
        Sum = sum;
    }

    public override void AddSum(float value)
    {
        if (CancelTransaction(Client))
        {
            throw new BanksException("Operation not possible");
        }

        if (value < LimitMinDegree)
        {
            throw new BanksException("sum doesn't exist");
        }

        Sum += value;
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

        if (value < LimitMinDegree || Sum - value < LimitMinDegree)
        {
            throw new BanksException("sum doesn't exist");
        }

        Sum -= value;
    }

    public override float GetPercentSum(DateTime period)
    {
        TimeSpan dateTime = period.Subtract(period);
        int mounth = dateTime.Days / 30;
        float refsum = Sum;
        while (mounth != LimitMinDegree)
        {
            refsum += Sum * (Bank.ClientInfo.GetPercentForDebit() / DaysInYear);
            mounth--;
        }

        return refsum;
    }

    public override void AddPercentSum()
    {
        if (DateTime.Today == CreatingTime.AddMonths(_monthCounting))
        {
            Sum += _percentSum;
            _percentSum = 0;
            _monthCounting += 1;
        }
    }

    public override void CountingPercentSum()
    {
        var everyDayCountTime = new TimeOnly(0, 0, 0);
        if (TimeOnly.FromDateTime(DateTime.Now) == everyDayCountTime)
        {
            _percentSum += Sum * (Bank.ClientInfo.GetPercentForDebit() / DaysInYear);
        }
    }
}