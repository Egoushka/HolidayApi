using HolidayApi.Data.Enum;
using Newtonsoft.Json.Linq;

namespace HolidayApi.Data;

public class Holiday
{
    public Holiday(JObject date, List<HolidayName> name, string holidayType)
    {
        Date = new Date(date);
        Name = name;
        HolidayType = holidayType;
    }

    public Date Date { get; set; }
    public List<HolidayName> Name { get; set; }
    public string HolidayType { get; set; }
}