using Newtonsoft.Json.Linq;
namespace HolidayApi.Data;

public class Country {
    public Country(string countryCode, List<string> regions, List<string> holidayTypes, string fullName, JObject fromDate, JObject toDate)
    {
        CountryCode = countryCode;
        Regions = regions;
        HolidayTypes = holidayTypes;
        FullName = fullName;
        FromDate = new Date(fromDate);
        ToDate = new Date(toDate);
    }

    public string CountryCode { get; set; }

    public List<string> Regions { get; set; }

    public List<string> HolidayTypes { get; set; }

    public string FullName { get; set; }

    public Date FromDate { get; set; }

    public Date ToDate { get; set; }
}