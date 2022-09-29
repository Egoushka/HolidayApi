using AutoMapper;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;
using HolidayApi.Data.DTO.Holiday;

namespace HolidayApi.Profiles;

public class HolidayProfile : Profile {

    public HolidayProfile()
    {
        CreateMap<Country, GetCountryDto>();
        CreateMap<Holiday, GetHolidayByYearAndCountryDto>();
    }
}