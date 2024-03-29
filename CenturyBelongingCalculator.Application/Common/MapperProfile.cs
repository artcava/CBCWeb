﻿using AutoMapper;
using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Domain;

namespace CenturyBelongingCalculator.Application.Common;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        //Event
        CreateMap<Event, EventModel>();
        //Calc
        CreateMap<Calc, CalcModel>();
    }
}
