namespace Banks.Observe;

public class ClientInfo
{
    private const int LimitDegree = 0;
    private int _transactionSumLimit;
    private float _percentForDebit;
    private Dictionary<int, float> _percentsForDeposit;
    private float _commissionForCredit;

    public ClientInfo(int transactionSumLimit, float percentForDebit, Dictionary<int, float> percentsForDeposit, float commissionForCredit)
    {
        if (transactionSumLimit < LimitDegree || percentForDebit < LimitDegree || percentsForDeposit is null ||
            commissionForCredit < LimitDegree)
        {
            throw new BanksException("Null reference of data");
        }

        _commissionForCredit = commissionForCredit;
        _percentForDebit = percentForDebit;
        _percentsForDeposit = percentsForDeposit;
        _transactionSumLimit = transactionSumLimit;
    }

    public void SetPercentForDebit(float value)
    {
        if (value < LimitDegree)
        {
            throw new BanksException("can't exist");
        }

        _percentForDebit = value;
    }

    public float GetPercentForDebit()
    {
        return _percentForDebit;
    }

    public void SetPercentForDeposit(Dictionary<int, float> value)
    {
        _percentsForDeposit = value ?? throw new BanksException("can't exist");
    }

    public Dictionary<int, float> GetPercentForDeposit()
    {
        return _percentsForDeposit;
    }

    public void SetCommission(float value)
    {
        if (value < LimitDegree)
        {
            throw new BanksException("can't exist");
        }

        _commissionForCredit = value;
    }

    public float GetCommision()
    {
        return _commissionForCredit;
    }

    public void SetTransaction(int value)
    {
        if (value < LimitDegree)
        {
            throw new BanksException("can't exist");
        }

        _transactionSumLimit = value;
    }

    public int GetTransaction()
    {
        return _transactionSumLimit;
    }
}