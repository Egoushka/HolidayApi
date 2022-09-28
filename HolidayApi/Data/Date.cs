using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HolidayApi.Data;

[Serializable]
public class Date {
    public Date(JObject date)
    {
        Year = Convert.ToInt32((string) date["year"]);
        Month = Convert.ToInt32((string) date["month"]);
        Day = Convert.ToInt32((string) date["day"]);
    }

    public Date(string day, string month, string year) {
        Day = int.Parse(day);
        Month = int.Parse(day);;
        Year = int.Parse(day);;
    }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}