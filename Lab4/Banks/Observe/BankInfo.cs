namespace Banks.Observe;

public class BankInfo
{
    private DateTime _dateTime;

    public BankInfo(DateTime dateTime)
    {
        _dateTime = dateTime;
    }

    public void SetDate(DateTime value)
    {
        _dateTime = value;
    }

    public DateTime GetDate()
    {
        return _dateTime;
    }
}