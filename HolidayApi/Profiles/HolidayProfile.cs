using AutoMapper;
using HolidayApi.Data;
using HolidayApi.Data.DTO.Country;

namespace HolidayApi.Profiles;

public class HolidayProfile : Profile {

    public HolidayProfile()
    {
        CreateMap<Country, GetCountryDto>();
    }
}