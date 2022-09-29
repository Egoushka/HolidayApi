using Newtonsoft.Json.Linq;

namespace HolidayApi.Data;

[Serializable]
public class Date {
    public Date(JObject date)
    {
        DayOfWeek = Convert.ToInt32((string) date["dayOfWeek"]);
        Year = Convert.ToInt32((string) date["year"]);
        Month = Convert.ToInt32((string) date["month"]);
        Day = Convert.ToInt32((string) date["day"]);
    }

    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int DayOfWeek { get; set; }
}