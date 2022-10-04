using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Data.Requests;
[BindProperties]
public class GetHolidaysByYearAndCountryRequest
{
    public string CountryCode { get; set; }
    public int Year { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(CountryCode, Year);
    }
}