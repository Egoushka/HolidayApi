namespace HolidayApi.Data.Requests;

public class GetSpecificDayStatusRequest
{
    public string Date { get; set; }
    public string CountryCode { get; set; }
    public override int GetHashCode()
    {
        return HashCode.Combine(Date, CountryCode);
    }
}