namespace HolidayApi.Data.DTO.Country;

public class GetCountryDto
{
    public GetCountryDto(Data.Country country)
    {
        FullName = country.FullName;
    }
    public string FullName { get; set; }
}