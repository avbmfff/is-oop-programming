namespace Banks;

public class Passport
{
    private const int LimitDegreeSeries = 4;
    private const int LimitDegreeNumber = 6;
    private readonly char[] _series;
    private char[] _number;
    private DateTime _registerDate;
    private string _placeOfIssue;

    public Passport(char[] series, char[] number, DateTime registerDate, string placeOfIssue)
    {
        if (series.Length != LimitDegreeSeries || number.Length != LimitDegreeNumber || !CheckForDigit(series) || !CheckForDigit(number))
        {
            throw new BanksException("Invalid series or number of passport. Please, try again");
        }

        if (string.IsNullOrWhiteSpace(placeOfIssue))
        {
            throw new BanksException("Null reference of place. Please, try again");
        }

        _series = series;
        _number = number;
        _registerDate = registerDate;
        _placeOfIssue = placeOfIssue;
    }

    public string GetSeriesAndNumber()
    {
        string number = _series.Aggregate(" ", (current, value) => current + value.ToString());

        return _series.Aggregate(number, (current, value) => current + value.ToString());
    }

    private bool CheckForDigit(IEnumerable<char> digits)
    {
        return digits.All(value => char.IsDigit(value));
    }
}