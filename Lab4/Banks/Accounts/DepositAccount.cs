namespace Banks.Accounts;

public class DepositAccount : Account
{
    private const int LimitMinDegree = 0;
    private const int DaysInYear = 365;
    private DateTime _finishTime;
    private float _starterSum;
    private float _percent;
    private float _percentSum = 0;
    private int _monthCounting = 0;

    public DepositAccount(Bank bank, Client.Client client, DateTime finishTime, float sum)
        : base(bank, client)
    {
        if (sum < LimitMinDegree)
        {
            throw new BanksException("sun can't exist");
        }

        Sum = sum;
        _finishTime = finishTime;
        _starterSum = Sum;
        GetPercent();
    }

    public override void AddSum(float value)
    {
        if (CancelTransaction(Client))
        {
            throw new BanksException("Operation not possible");
        }

        if (DateTime.Today != _finishTime || value < LimitMinDegree)
        {
            throw new BanksException("can't so this");
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

        if (DateTime.Today != _finishTime || value < LimitMinDegree)
        {
            throw new BanksException("can't so this");
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
            refsum += Sum * (_percent / DaysInYear);
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
            _percentSum += Sum * (_percent / DaysInYear);
        }
    }

    private void GetPercent()
    {
        float prev = 0;
        foreach (KeyValuePair<int, float> value in Bank.ClientInfo.GetPercentForDeposit())
        {
            if (value.Key > _starterSum)
            {
                _percent = prev;
            }

            prev = value.Value;
        }
    }
}