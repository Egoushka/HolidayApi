namespace HolidayApi.Data.Requests;

public class GetMaximumNumberOfFreeDaysRequest
{
    public int Year { get; set; }
    public string CountryCode { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(Year, CountryCode);
    }
}