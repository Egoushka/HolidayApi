using Microsoft.AspNetCore.Mvc;

namespace HolidayApi.Data.Requests;
[BindProperties]
public class GetHolidaysByYearAndCountryRequest
{
    public string CountryCode { get; set; }
    public int Year { get; set; }
}