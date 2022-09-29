using HolidayApi.Data.Enum;

namespace HolidayApi.Data.DTO.Holiday;

public class GetHolidayByYearAndCountryDto
{
    public Date Date { get; set; }
    public List<HolidayName> Name { get; set; }
    public string HolidayType { get; set; }
}